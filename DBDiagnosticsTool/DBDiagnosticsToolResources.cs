using Telerik.Sitefinity.Localization;

namespace DBDiagnostics
{
    /// <summary>
    /// Localizable strings for the DB Diagnostics Tool module
    /// </summary>
    [ObjectInfo(typeof(DBDiagnosticsToolResources), ResourceClassId = "DBDiagnosticsToolResources", Title = "DBDiagnosticsResourcesTitle", Description = "DBDiagnosticsResourcesDescription")]
    public class DBDiagnosticsToolResources : Resource
    {
        /// <summary>
        /// Phrase: DB Diagnostics tool resources.
        /// </summary>
        [ResourceEntry("DBDiagnosticsResourcesTitle",
            Value = "DB Diagnostics tool resources",
            Description = "Title for the DB Diagnostic tools resources class.",
            LastModified = "2019/05/13")]
        public string DBDiagnosticsResourcesTitle
        {
            get
            {
                return this["DBDiagnosticsResourcesTitle"];
            }
        }

        /// <summary>
        /// Phrase: Localizable strings for the DB Diagnostic tools.
        /// </summary>
        [ResourceEntry("DBDiagnosticsResourcesDescription",
            Value = "Localizable strings for the DB Diagnostic tools.",
            Description = "Description for the DB Diagnostic tools resources class.",
            LastModified = "2019/05/13")]
        public string DBDiagnosticsResourcesDescription
        {
            get
            {
                return this["DBDiagnosticsResourcesDescription"];
            }
        }

        /// <summary>
        /// Phrase: DB Diagnostics
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolGroupPageTitle",
                        Value = "DB Diagnostics",
                        Description = "The title of the DB Diagnostics Tool group page.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolGroupPageTitle
        {
            get
            {
                return this["DBDiagnosticsToolGroupPageTitle"];
            }
        }

        /// <summary>
        /// Phrase: DBDiagnostics
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolGroupPageUrlName",
                        Value = "DBDiagnostics",
                        Description = "The DBDiagnostics Tool group page URL.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolGroupPageUrlName
        {
            get
            {
                return this["DBDiagnosticsToolGroupPageUrlName"];
            }
        }

        /// <summary>
        /// Phrase: DBDiagnostics
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolPageUrlName",
                        Value = "DBDiagnostics",
                        Description = "The DBDiagnostics Tool page URL.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolPageUrlName
        {
            get
            {
                return this["DBDiagnosticsToolPageUrlName"];
            }
        }

        /// <summary>
        /// Phrase: Group page for the  of the DB Diagnostics Tool.
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolGroupPageDescription",
                        Value = "DB Diagnostics",
                        Description = "DB Diagnostics Tool group page description.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolGroupPageDescription
        {
            get
            {
                return this["DBDiagnosticsToolGroupPageDescription"];
            }
        }

        /// <summary>
        /// Phrase: DBDiagnosticsToolPage
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolPage",
                        Value = "DB Diagnostics",
                        Description = "Landing page for the  of the DB Diagnostics Tool.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolPage
        {
            get
            {
                return this["DBDiagnosticsToolPage"];
            }
        }

        /// <summary>
        /// Phrase: DB Diagnostics Tool
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolPageTitle",
                        Value = "DB Diagnostics Tool",
                        Description = "The title of the DB Diagnostics Tool page.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolPageTitle
        {
            get
            {
                return this["DBDiagnosticsToolPageTitle"];
            }
        }

        /// <summary>
        /// Phrase: DB Diagnostics
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolPageDescription",
                        Value = "DB Diagnostics",
                        Description = "DB Diagnostics Tool page description.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolPageDescription
        {
            get
            {
                return this["DBDiagnosticsToolPageDescription"];
            }
        }

        /// <summary>
        /// Phrase: DB Diagnostics
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolboxSectionTitle",
                        Value = "DB Diagnostics",
                        Description = "DB Diagnostics tools section title.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolboxSectionTitle
        {
            get
            {
                return this["DBDiagnosticsToolboxSectionTitle"];
            }
        }

        /// <summary>
        /// Phrase: Tools for DB Diagnostics
        /// </summary>
        [ResourceEntry("DBDiagnosticsToolboxSectionDescription",
                        Value = "Tools for DB Diagnostics",
                        Description = "DB Diagnostics tools section description.",
                        LastModified = "2019/05/13")]
        public string DBDiagnosticsToolboxSectionDescription
        {
            get
            {
                return this["DBDiagnosticsToolboxSectionDescription"];
            }
        }

        

        /// <summary>
        /// Phrase: "Widget description goes here..."
        /// </summary>
        [ResourceEntry("WidgetDescription",
            Value = "Widget description goes here...",
            Description = "Widget description goes here...",
            LastModified = "2019/05/13")]
        public string WidgetDescription
        {
            get
            {
                return this["WidgetDescription"];
            }
        }

        /// <summary>
        /// Phrase: "Database diagnostics"
        /// </summary>
        [ResourceEntry("DashboardDBDiagnosticsViewTitle",
            Value = "Database diagnostics",
            Description = "Database diagnostics",
            LastModified = "2019/05/13")]
        public string DashboardDBDiagnosticsViewTitle
        {
            get
            {
                return this["DashboardDBDiagnosticsViewTitle"];
            }
        }

        /// <summary>
        /// Phrase: Rebuild index
        /// </summary>
        [ResourceEntry("RebuildIndexLink",
            Value = "Rebuild index",
            Description = "Rebuild index link title",
            LastModified = "2019/05/13")]
        public string RebuildIndexLink
        {
            get
            {
                return this["RebuildIndexLink"];
            }
        }

        /// <summary>
        /// Phrase: Get database tables
        /// </summary>
        [ResourceEntry("DatabaseTablesTitle",
            Value = "Get database tables",
            Description = "Get database tables",
            LastModified = "2019/05/13")]
        public string DatabaseTablesTitle
        {
            get
            {
                return this["DatabaseTablesTitle"];
            }
        }
    }
}