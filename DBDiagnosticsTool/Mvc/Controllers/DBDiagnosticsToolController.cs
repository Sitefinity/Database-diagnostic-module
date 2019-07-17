using DBDiagnostics.Database;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.Services;

namespace DBDiagnostics.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the DB Diagnostics tool.
    /// </summary>
    [Localization(typeof(DBDiagnosticsToolResources))]
    [ControllerToolboxItem(Name = "DBDiagnosticsTool", Title = "DB Diagnostics Tool", SectionName = "DB Diagnostics", CssClass = "sfMvcIcn")]
    public class DBDiagnosticsToolController : Controller
    {
        //public DBDiagnosticsToolController(IDatabaseDiagnosticsService dbService)
        //{
        //    this.dbService = dbService;
        //}

        public DBDiagnosticsToolController()
        {
            this.dbService = new DatabaseDiagnosticsService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("db-diagnostics/rebuild-indexes")]
        public async Task<JsonResult> RebuildIndexes()
        {
            ServiceUtility.RequestAuthentication(true);
            await dbService.RebuildIndexes();
            return this.Json(new { result = new HttpStatusCodeResult(HttpStatusCode.OK) }, JsonRequestBehavior.AllowGet);
        }

        [Route("db-diagnostics/database-size")]
        public async Task<JsonResult> GetDatabaseSize()
        {
            ServiceUtility.RequestAuthentication(true);
            var result = await dbService.GetDBSize();
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        [Route("db-diagnostics/database-tables")]
        public async Task<JsonResult> GetDatabaseTables()
        {
            ServiceUtility.RequestAuthentication(true);
            var result = await dbService.GetDatabaseTables();
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        [Route("db-diagnostics/index-fragmentation")]
        public async Task<JsonResult> GetIndexFragmentation()
        {
            ServiceUtility.RequestAuthentication(true);
            var result = await dbService.GetIndexFragmentation();
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        private DatabaseDiagnosticsService dbService;
    }
}
