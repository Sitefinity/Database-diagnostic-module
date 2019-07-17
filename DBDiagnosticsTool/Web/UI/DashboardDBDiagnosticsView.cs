using DBDiagnostics.Database;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace DBDiagnostics.Web.UI
{
    /// <summary>
    /// A widget that displays all items supported by the Dashboard module.
    /// </summary>
    public class DashboardDBDiagnosticsView : SimpleView
    {
        /// <inheritdoc />
        public override string LayoutTemplatePath
        {
            get
            {
                var templatePath = base.LayoutTemplatePath;
                if (string.IsNullOrEmpty(templatePath))
                    return string.Concat(DBDiagnosticsModule.DBDiagnosticsVirtualPath, DashboardDBDiagnosticsView.TemplatePath);
                return templatePath;
            }

            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <inheritdoc />
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        #region Methods

        /// <inheritdoc />
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.IsDesignMode() && !this.IsPreviewMode())
            {
                this.Controls.Clear();
            }
        }

        /// <inheritdoc />
        protected override void InitializeControls(GenericContainer container)
        {
            var top5LargestTables = Task.Run(async () => await dbService.GetDatabaseTables()).Result.OrderByDescending(p => p.Size).Take(5);
            foreach (var table in top5LargestTables)
            {
                var row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell() { InnerText = table.Name });
                row.Cells.Add(new HtmlTableCell() { InnerText = table.Rows.ToString() });
                row.Cells.Add(new HtmlTableCell() { InnerText = string.Format("{0:0.00}", (double)table.Size / 1024) });
                this.DBTablesGrid.Rows.Add(row);
            }

            var database = Task.Run(async () => await dbService.GetDBSize()).Result;
            this.DBName.Text = database.Name;
            this.DBSize.Text = database.Size;
            this.DBUnallocated.Text = database.Unallocated;
        }

        protected HtmlTable DBTablesGrid
        {
            get
            {
                return this.Container.GetControl<HtmlTable>("dbTablesGrid", true);
            }
        }
        protected Literal DBName
        {
            get
            {
                return this.Container.GetControl<Literal>("dbName", true);
            }
        }

        protected Literal DBSize
        {
            get
            {
                return this.Container.GetControl<Literal>("dbSize", true);
            }
        }

        protected Literal DBUnallocated
        {
            get
            {
                return this.Container.GetControl<Literal>("dbUnallocated", true);
            }
        }

        /// <inheritdoc />
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                // We use div wrapper tag to make easier common styling
                return HtmlTextWriterTag.Div;
            }
        }

        #endregion

        #region Private members & constants

        private const string TemplatePath = "DBDiagnostics.Web.UI.DashboardDBDiagnosticsView.ascx";
        private static DatabaseDiagnosticsService dbService = new DatabaseDiagnosticsService();

        #endregion
    }
}