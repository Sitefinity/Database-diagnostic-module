using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDiagnostics.Database.Model
{
    /// <summary>
    /// Index information
    /// </summary>
    public class Index
    {
        /// <summary>
        /// Name of the index
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The database the index belongs to
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// The table the index is part of
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// The index fragmentaion percentage.
        /// </summary>
        public float Fragmentation { get; set; }

        /// <summary>
        /// Index fill factor (between 0 and 100)
        /// </summary>
        public int FillFactor { get; set; }
    }
}
