using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBDiagnostics.Database;
using MbUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [Description("Some simple tests.")]
    public class DBDiagnosticsTests
    {
        [Test]
        [Category(TestCategories.DBDiagnostics)]
        [Author("Progress Sitefinity")]
        public void AssertDBDiagnosticsSerivceReturnsDatabaseTables()
        {
            var dbDiagnosticsService = new DatabaseDiagnosticsService();
            var dbTables = Task.Run(async () => await dbDiagnosticsService.GetDatabaseTables()).Result;
            Assert.IsNotNull(dbTables);
            Assert.IsTrue(dbTables.Count > 0);
        }

        [Test]
        [Category(TestCategories.DBDiagnostics)]
        [Author("Progress Sitefinity")]
        public void AssertDBDiagnosticsSerivceReturnsDBSize()
        {
            var dbDiagnosticsService = new DatabaseDiagnosticsService();
            var dbSize = Task.Run(async () => await dbDiagnosticsService.GetDBSize()).Result;
            Assert.IsNotNull(dbSize);
            Assert.IsNotNull(dbSize.Name);
            Assert.IsNotNull(dbSize.Size);
        }
    }
}
