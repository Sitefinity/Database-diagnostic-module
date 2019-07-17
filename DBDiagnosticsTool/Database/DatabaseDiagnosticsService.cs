using DBDiagnostics.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Configuration;

namespace DBDiagnostics.Database
{
    /// <summary>
    /// Provides database maintenance functions.
    /// </summary>
    public class DatabaseDiagnosticsService
    {
        public DatabaseDiagnosticsService()
        {
            var dataConfig = Telerik.Sitefinity.Configuration.Config.Get<DataConfig>();
            IConnectionStringSettings connString;
            if (dataConfig.TryGetConnectionString(DataConfig.DefaultConnectionName, out connString))
            {
                this.connectionString = connString.ConnectionString;
            }
            else
            {
                // Log: No connection string available
                Telerik.Sitefinity.Abstractions.Log.Write("No connection string available", TraceEventType.Error);
            }
        }

        /// <summary>
        /// Returns information about database tables.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Table>> GetDatabaseTables()
        {
            var result = new List<Table>();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    // set timeout to 5 mins
                    cmd.CommandTimeout = 5 * 60;
                    cmd.CommandText = @"
DECLARE @tableUsageTable TABLE (
	tName nvarchar(100), 
	tRows nvarchar(20), 
	tReserved nvarchar(30), 
	tData nvarchar(30),
	tIndexSize nvarchar(30),
	tUnused nvarchar (15))

DECLARE @tableName nvarchar(100)

DECLARE tableCursor CURSOR FAST_FORWARD 
FOR SELECT [name] FROM sys.tables

DECLARE @sql nvarchar (1000) 
SET @sql = N'EXEC sp_spaceused @tableName, false'
DECLARE @ParmDefinition nvarchar(1000)
SET @ParmDefinition = N'@tableName nvarchar(200)';

OPEN tableCursor

FETCH NEXT FROM tableCursor 
INTO @tableName

WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO @tableUsageTable 
	EXEC sp_executesql @sql, @ParmDefinition, @tableName = @tableName
	
	FETCH NEXT FROM tableCursor 
	INTO @tableName
END

CLOSE tableCursor
DEALLOCATE tableCursor

SELECT 

tName as Name
, CAST (tRows AS int) AS totalRows
, CAST (Substring(tReserved , 1, Len(tReserved ) - 3) AS int) as totalSize
FROM @tableUsageTable

ORDER BY 
	totalSize DESC
";
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var tableNeme = reader.GetString(0);
                                var totalRows = reader.GetInt32(1);
                                var totalSize = reader.GetInt32(2);

                                result.Add(new Table() { Name = tableNeme, Rows = totalRows, Size = totalSize });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(string.Format("Failed to get database table information. Message: {0}", ex.Message), TraceEventType.Error);
            }

            return result;
        }

        public async Task RebuildIndexes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    // set timeout to 5 mins
                    cmd.CommandTimeout = 10 * 60;
                    cmd.CommandText = @"
EXEC sp_MSforeachtable ' DBCC DBREINDEX (''?'') '
";

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Write(string.Format("Failed to rebuild database indexes. Message: {0}", ex.Message), TraceEventType.Error);
            }
        }

        public async Task<DBDiagnostics.Database.Model.Database> GetDBSize()
        {
            var result = new Model.Database();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    // set timeout to 5 mins
                    cmd.CommandTimeout = 10 * 60;
                    cmd.CommandText = @"
EXEC sp_spaceused @oneresultset = 1;
";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Name = reader.GetString(0);
                                result.Size = reader.GetString(1);
                                result.Unallocated = reader.GetString(2);
                                result.Reserved = reader.GetString(3);
                                result.Data = reader.GetString(4);
                                result.Index = reader.GetString(5);
                                result.Unused = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(string.Format("Failed to rebuild database indexes. Message: {0}", ex.Message), TraceEventType.Error);
            }
            return result;
        }

        /// <summary>
        /// Returns information about database tables.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Index>> GetIndexFragmentation()
        {
            var result = new List<Index>();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    // set timeout to 5 mins
                    cmd.CommandTimeout = 5 * 60;
                    cmd.CommandText = @"
	SELECT 
		DB.[Name] AS [DataBase]
		, SO.[Name] AS TableName
		, SI.name as IndexName
		, Cast(IDX.[avg_fragmentation_in_percent] as INT) as IndexFragmentation
		, SI.fill_factor
	FROM sys.dm_db_index_physical_stats (
		DB_ID(), 
		NULL,
		NULL, 
		NULL, 
		NULL 
		) AS IDX
	INNER JOIN sys.objects SO ON IDX.object_id = SO.object_id
	INNER JOIN sys.indexes SI ON IDX.index_id = SI.index_id AND IDX.object_id = SI.object_id
	INNER JOIN sys.databases DB ON IDX.database_id = DB.database_id
	ORDER BY avg_fragmentation_in_percent DESC
";
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var dbName = reader.GetString(0);
                                var tableName = reader.GetString(1);
                                var indexName = reader.GetString(2);
                                var fragmentation = reader.GetInt32(3);
                                var fillFactor = reader.GetByte(4);
                                result.Add(

                                    new Index() {
                                        Database = dbName,
                                        Table = tableName,
                                        Name = indexName,
                                        Fragmentation = fragmentation,
                                        FillFactor = fillFactor
                                    });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(string.Format("Failed to get index information. Message: {0}", ex.Message), TraceEventType.Error);
            }

            return result;
        }

        private string connectionString;
    }
}
