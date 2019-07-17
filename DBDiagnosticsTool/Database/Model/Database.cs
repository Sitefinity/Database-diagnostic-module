using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDiagnostics.Database.Model
{
    /// <summary>
    /// Provides information for a database size
    /// </summary>
    public class Database
    {
        /// <summary>
        /// The name of the Database
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The database size
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The unallocated space for the database
        /// </summary>
        public string Unallocated { get; set; }

        /// <summary>
        /// The reserved space
        /// </summary>
        public string Reserved { get; set; }

        /// <summary>
        /// The space for the data 
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The index size
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// The unused space
        /// </summary>
        public string Unused { get; set; }
    }
}
