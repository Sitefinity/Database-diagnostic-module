using DBDiagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Services;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DBDiagnostics")]
[assembly: AssemblyDescription("Provides database overview and maintenance operations for Sitefinity.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Progress")]
[assembly: AssemblyProduct("DBDiagnostics")]
[assembly: AssemblyCopyright("Copyright © Progress 2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("79e139e7-4212-4441-aaeb-848df5517cb8")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: SitefinityModule("DBDiagnosticsTool", typeof(DBDiagnosticsModule), "DB Diagnostics Tool", "Provides database overview and maintenance operations.", StartupType.OnApplicationStart)]
[assembly: ControllerContainer]
