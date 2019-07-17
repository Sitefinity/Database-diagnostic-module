<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<sf:ResourceLinks ID="resourcesLinks" runat="server" UseEmbeddedThemes="True" UseBackendTheme="True">
    <sf:ResourceFile JavaScriptLibrary="JQuery" />
    <sf:ResourceFile JavaScriptLibrary="KendoAll" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js" Static="True" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Web.SitefinityJS.Telerik.Sitefinity.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
</sf:ResourceLinks>

<div class="sfDashboardWidgetWrp" runat="server">
    <h2 class="sfBlack">DB Diagnostics</h2>
    <div class="dataWrapper">
        <dl class="sfLicenseInfo sfPTop15 sfPBottom15">
            <dt>Database
            </dt>
            <dd>
                <asp:Literal runat="server" id="dbName"></asp:Literal>
            </dd>
            <dt>Size
            </dt>
            <dd>
                <asp:Literal runat="server" id="dbSize"></asp:Literal>
            </dd>
            <dt>Unallocated
            </dt>
            <dd>
                <asp:Literal runat="server" id="dbUnallocated"></asp:Literal>
            </dd>
        </dl>
        <div class="k-grid k-widget k-display-block">
            <table id="dbTablesGrid" runat="server">
                <tr class="k-grid-header">
                    <th class="k-header">Table (Top 5 largest)</th>
                    <th class="k-header">Rows Count</th>
                    <th class="k-header">Size (MB)</th>
                </tr>
            </table>
        </div>
    </div>
    <a class="sfGoto" href="/Sitefinity/Administration/DBDiagnostics" target="_blank">Go to DB diagnostics</a>
</div>
