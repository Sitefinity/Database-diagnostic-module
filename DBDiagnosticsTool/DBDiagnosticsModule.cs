using DBDiagnostics.Config;
using DBDiagnostics.Database;
using DBDiagnostics.Mvc.Controllers;
using DBDiagnostics.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Dashboard;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;


namespace DBDiagnostics
{
    /// <summary>
    /// DB Diagnostics module
    /// Analyzes DB indexes fragmentation
    /// Enables administrators to rebuild the DB indexes
    /// Summarizes DB information such as name, overall size, space distribution, tables size and #rows
    /// The module demonstrates how the Sitefintiy CMS module architecture can be used with minumum implementation effort
    /// To add basic functionality such as:
    /// - pluggable, self-installable module
    /// - that executes some custom logic
    /// - with a backend page
    /// - and a dashboard widget
    /// </summary>
    /// 

    public class DBDiagnosticsModule : ModuleBase
    {
        //Provide the GUID of the desired landing page for your module
        //The LandingPageId specifies which is the default module page
        //This is the entry point for the module UI. It is usually added under the Administration menu
        //Some modules may not have landing pages, or have them hiddedn, depending on the use case scenario
        public override Guid LandingPageId
        {
            get
            {
                return DBDiagnosticsModule.DBDiagnosticsPageId;
            }
        }

        //Provide a list of managers your module will be working with
        //It might be your own managers or default Sitefinity ones
        //Sitefintiy CMS uses this collection to make sure the managers (and their respective providers) are initialized
        //This way you can safely work with the managers inside your module logic
        public override Type[] Managers
        {
            get
            {
                return new Type[] { typeof(PageManager) };
            }
        }

        //This method is called every time your module initializes
        //For example when your Sitefinity website restarts
        //here you confgigure your module settings, configurations, resources and so on
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            App.WorkWith()
                .Module(settings.Name)
                .Initialize()
                .Configuration<DBDiagnosticsConfig>()
                .Localization<DBDiagnosticsToolResources>();
        }

        //Add any virtual paths that you want to be automatically registered
        //Virtual paths are used to resolve embedded resources from the assemblies, emulating a folder strucutre
        //If no virtual path is specified, the default EmbeddedResourceResolver and your module's assembly name are used
        protected override IDictionary<string, Action<VirtualPathElement>> GetVirtualPaths()
        {
            var paths = new Dictionary<string, Action<VirtualPathElement>>();
            paths.Add(DBDiagnosticsVirtualPath + "*", null);
            return paths;
        }

        //If your module needs to store any configurations, load them here
        //The actual implementation is bset separated in a separate config class
        //In this case the /Config/DBDiagnosticsConfig.cs is empty as we don't need configs, but is added for demo purposes
        protected override ConfigSection GetModuleConfig()
        {
            return Telerik.Sitefinity.Configuration.Config.Get<DBDiagnosticsConfig>();
        }

        //The install method is called initially, when your module is added to your Sitefinity website
        //Here you can install your module configurations, add the module pages and widgets
        public override void Install(SiteInitializer initializer)
        {
            //This logic below is meant to verify that you're usign the module with the supported DB types only
            //Namely MS SQL and MS Azure
            //If other DB type is detected, the module will throw an exception
            var oaProvider = initializer.MetadataManager.Provider as IOpenAccessDataProvider;
            if (oaProvider != null)
            {
                var context = oaProvider.GetContext();
                if (context != null &&
                        (context.Metadata.BackendType != Telerik.OpenAccess.Metadata.Backend.MsSql &&
                        context.Metadata.BackendType != Telerik.OpenAccess.Metadata.Backend.Azure)
                    )
                {
                    throw new ApplicationException(string.Format("Unsupported database type: {0}", Enum.GetName(typeof(Telerik.OpenAccess.Metadata.Backend), context.Metadata.BackendType)));
                }
            }

            InstallConfiguration(initializer);
            InstallNewDBDiagnosticsPage(initializer);
            //Here we are adding our DB Diagnosticss widget to the Dashboard backend page
            var pageNode = initializer.PageManager.GetPageNodes().Where(x => x.Id == DashboardModule.DashboardPageNodeId).FirstOrDefault();
            AddDashboardDBDiagnosticsWidgetToPage(initializer, pageNode);
        }

        //Here you can implement any configurations-related logic
        //In our case we are registering the DB Diagnostics widget 
        //In the Backend page widgets section
        //So it can be manually added to a backend page
        private void InstallConfiguration(SiteInitializer initializer)
        {
            initializer.Installer
                .Toolbox(CommonToolbox.PageWidgets)
                       .LoadOrAddSection("DB Diagnostics")
                       .LocalizeUsing<DBDiagnosticsToolResources>()
                       .SetTitle("DBDiagnosticsToolboxSectionTitle")
                       .SetDescription("DashboardToolboxSectionDescription")
                       .SetTags(ToolboxTags.Backend)
                       .SetOrdinal(-1);
        }

        #region Module backend page and widget
        //Here we are instantinating and configuring our DB Diagnostics widget
        //Creating a new backend group page and placing it under the main Administration menu, under the Tools section
        //This page only serves as a placeholder in the backend navigation menu
        //After that we are creatign a standard backend page 
        //That contains the actual module UI
        //It is a child of the first page we created, and is hidden from the naviagtion
        //Instead, it is opened, when a user clicks on the group page from Administration menu
        //Finally we are addingthe instantiated and configured DB diagnostics widget on the page
        private void InstallNewDBDiagnosticsPage(SiteInitializer initializer)
        {
            Guid siblingId = Guid.Empty;
            var dbDiagnosticToolWidget = new MvcControllerProxy();
            dbDiagnosticToolWidget.ControllerName = typeof(DBDiagnosticsToolController).FullName;
            dbDiagnosticToolWidget.Settings = new ControllerSettings(new DBDiagnosticsToolController());

            initializer.Installer
                .CreateModuleGroupPage(ModuleGroupPageId, "DBDiagnosticsTool")
                    .PlaceUnder(SiteInitializer.ToolsNodeId)
                    .SetOrdinal(6f)
                    .LocalizeUsing<DBDiagnosticsToolResources>()
                    .SetTitleLocalized("DBDiagnosticsToolGroupPageTitle")
                    .SetUrlNameLocalized("DBDiagnosticsToolGroupPageUrlName")
                    .SetDescriptionLocalized("DBDiagnosticsToolGroupPageDescription")
                    .ShowInNavigation()
                    .AddChildPage(DBDiagnosticsPageId, "DBDiagnosticsToolPage")
                        .SetOrdinal(1)
                        .LocalizeUsing<DBDiagnosticsToolResources>()
                        .SetTitleLocalized("DBDiagnosticsToolPageTitle")
                        .SetHtmlTitleLocalized("DBDiagnosticsToolPageTitle")
                        .SetUrlNameLocalized("DBDiagnosticsToolPageUrlName")
                        .SetDescriptionLocalized("DBDiagnosticsToolPageDescription")
                        .AddControl(dbDiagnosticToolWidget)
                        .HideFromNavigation()
                    .Done();
        }

        //Here we are taking care of adding the Db diagnostics dashboard widget to the Sitefintiy dashboard
        //First, we are registering our widget in the backend pages toolbox
        //Then we are instantiating the Db diagnostics dashboard widget
        //And adding it to the Dashboard backend page
        //Note that we must specify a valid placeholder where the widget should be placed
        //We must also specify the widget caption
        //Another specific you should pay attention to is the permissions configuration for the widget
        //In this case we are making sure the widget is visible only to Administrators
        private void AddDashboardDBDiagnosticsWidgetToPage(SiteInitializer initializer, PageNode pageNode)
        {
            initializer.Installer
                       .Toolbox(CommonToolbox.PageWidgets)
                       .LoadOrAddSection("Dashboard")
                       .LocalizeUsing<Labels>()
                       .SetTitle("DashboardToolboxSectionTitle")
                       .SetDescription("DashboardToolboxSectionDescription")
                       .SetTags(ToolboxTags.Backend)
                       .SetOrdinal(-1) // should be first
                       .LoadOrAddWidget<DashboardDBDiagnosticsView>("DashboardDBDiagnosticsView")
                       .SetTitle("DashboardDBDiagnosticsViewTitle")
                       .SetDescription("DashboardDBDiagnosticsViewDescription")
                       .LocalizeUsing<DBDiagnosticsToolResources>()
                       .Done();

            var dashboardSystemStatusView = new DashboardDBDiagnosticsView();
            dashboardSystemStatusView.ID = "dashboardDBDiagnosticsView";

            if (pageNode != null)
            {
                var topWidgetLayout = pageNode.GetPageData().Controls.Where(c => c.IsLayoutControl && c.PlaceHolders.Length > 0 &&
                                                                                          c.PlaceHolders[0].StartsWith("WidgetsLayoutTop")).FirstOrDefault();
                if (topWidgetLayout != null)
                {
                    var placeholder = topWidgetLayout.PlaceHolders[0];
                    PageDraftControl pageDraftControl = initializer.PageManager.CreateControl<PageDraftControl>(dashboardSystemStatusView, placeholder);
                    pageDraftControl.Caption = Res.Get<DBDiagnosticsToolResources>().DashboardDBDiagnosticsViewTitle;
                    pageDraftControl.Permissions.Clear(); // Should be visible only for administrators
                    AddWidgetToDashboard(initializer.PageManager, pageNode, pageDraftControl, new string[] { "WidgetsLayoutTop" }, true, placeHolderIndex: 1);
                }
            }
        }

        #region Helper methods to facilitate adding the widget on a page
        //Helper method making sure all draft version sof the page we are editing are cleared
        private void RemoveDraftsVersions(PageNode pageNode, PageManager pageManager)
        {
            PageData dashBoardPageData = pageNode.GetPageData();

            pageManager.PagesLifecycle.DiscardAllDrafts(dashBoardPageData);
        }

        //Helper method helping locate the desired layout control, whcih will be used as a placeholder for our widget
        private PageDraftControl GetTargetedControl(string[] targetedLayoutPlaceHolders, PageDraft dashboard, int placeHolderIndex)
        {
            PageDraftControl targetedControl = null;
            for (int layoutIndex = 0; layoutIndex < targetedLayoutPlaceHolders.Length; layoutIndex++)
            {
                var targetedLayoutPlaceHolder = targetedLayoutPlaceHolders[layoutIndex];

                foreach (var dashboardControl in dashboard.Controls)
                {
                    if (dashboardControl.IsLayoutControl)
                    {
                        if (dashboardControl.PlaceHolders != null && dashboardControl.PlaceHolders.Length > placeHolderIndex)
                        {
                            if (dashboardControl.PlaceHolders[placeHolderIndex].StartsWith(targetedLayoutPlaceHolder))
                            {
                                targetedControl = dashboardControl;
                                break;
                            }
                        }
                    }
                }

                if (targetedControl != null)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            return targetedControl;
        }

        //Helper method facilitating the logic of adding a widget to a page
        private void AddWidgetToDashboard(PageManager pageManager, PageNode pageNode, PageDraftControl control, string[] targetedLayoutPlaceHolders, bool skipIfContainsControl = false, int placeHolderIndex = 0)
        {
            RemoveDraftsVersions(pageNode, pageManager);
            var dashboard = pageManager.PagesLifecycle.Edit(pageNode.GetPageData());

            PageDraftControl targetedControl = GetTargetedControl(targetedLayoutPlaceHolders, dashboard, placeHolderIndex);

            if (targetedControl == null)
            {
                return;
            }

            var layoutPlaceholder = targetedControl.PlaceHolders[placeHolderIndex];
            var controlsWithSamePlaceholder = dashboard.Controls.Where(c => c.PlaceHolder == layoutPlaceholder);
            var containsControl = controlsWithSamePlaceholder.Any(c => c.ObjectType == control.ObjectType);
            if (!containsControl || (containsControl && !skipIfContainsControl))
            {
                PageDraftControl firstControl = null;

                if (controlsWithSamePlaceholder != null)
                {
                    firstControl = controlsWithSamePlaceholder.Where(c => c.SiblingId.Equals(Guid.Empty)).FirstOrDefault();
                }

                control.PlaceHolder = targetedControl.PlaceHolders[placeHolderIndex];
                control.SiblingId = Guid.Empty;

                if (firstControl != null)
                {
                    firstControl.SiblingId = control.Id;
                }

                dashboard.Controls.Add(control);

                dashboard.ApprovalWorkflowState.Value = "Published";
                pageManager.PagesLifecycle.Publish(dashboard);
            }
            else
            {
                RemoveDraftsVersions(pageNode, pageManager);
            }
        }
        #endregion

        #endregion

        #region Analysis and Logging logic
        private void LogInitialState()
        {
            LogDbSize();
            LogTableSizes();
            LogIndexFragmentation();
        }

        private void LogIndexFragmentation()
        {
            var dbService = new DatabaseDiagnosticsService();
            var indexes = dbService.GetIndexFragmentation().Result;
            StringBuilder sb = new StringBuilder("Index info: Database, Table, Index, Fragmentation, Fill Factor: ");
            sb.AppendLine();
            foreach (var idx in indexes)
            {
                sb.AppendLine(string.Format("{0, 35} {1, 35} {2, 35} {3, 10} {4, 10}",
                    idx.Database, idx.Table, idx.Name, idx.Fragmentation, idx.FillFactor));
            }

            Log.Write(sb.ToString(), System.Diagnostics.TraceEventType.Information);
        }

        private void LogDbSize()
        {
            var dbService = new DatabaseDiagnosticsService();
            var dataBase = dbService.GetDBSize().Result;
            StringBuilder sb = new StringBuilder("Database info:");
            sb.AppendLine();
            sb.AppendLine(string.Format("Database: {0}, Size: {1}, Reserved: {2}, Unallocated: {3}, Data: {4}, Index: {5}, Unused {6}",
                dataBase.Name, dataBase.Size, dataBase.Reserved, dataBase.Unallocated, dataBase.Data, dataBase.Index, dataBase.Unused));

            Log.Write(sb.ToString(), System.Diagnostics.TraceEventType.Information);
        }

        private void LogTableSizes()
        {
            var dbService = new DatabaseDiagnosticsService();
            var tables = dbService.GetDatabaseTables().Result;
            StringBuilder sb = new StringBuilder("Database Tables info:");
            sb.AppendLine();
            foreach (var t in tables)
            {
                sb.AppendLine(string.Format("{0, 35}, {1, 15}, {2, 15}", t.Name, t.Rows, t.Size));
            }

            Log.Write(sb.ToString(), System.Diagnostics.TraceEventType.Information);
        }

        #endregion

        #region Constants
        public static readonly Guid ModuleGroupPageId = new Guid("0A0431F1-CE1C-4C25-96F3-B8A467FE07F4");
        public static readonly Guid DBDiagnosticsPageId = new Guid("0A0431F1-CE1C-4C25-96F3-B8A467FE07F5");
        public const string DBDiagnosticsPageName = "Database Diagnostics Tool";
        public const string moduleName = "DBDiagnosticsModule";

        public const string DBDiagnosticsVirtualPath = "~/DBDiagnostics/";
        #endregion
    }
}
