using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDiagnostics.Database.Model
{
    /// <summary>
    /// Represents a database table.
    /// </summary>
    public class Table
    {
        /// <summary>
        /// The name of the database table.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Total size in kB.
        /// </summary>
        public int Size { get; set; }
     
        /// <summary>
        /// The number of rows 
        /// </summary>
        public int Rows { get; set; }
    }
}
