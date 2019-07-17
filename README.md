# Database-diagnostic-module

## Overview
The Database diagnostic module integrates with your Sitefinity website and provides administrators with valuable insight about the website database health. It displays a summary of database information such as:
* DB Name
* DB overall size
* Space distribution
* Tables size
* Number of rows
* Indexes fragmentation

The module displays the above describved information on the Sitefinity dashboard and enables administrators to rebuild database indexes with a click of a button.

## Sample purpose
This code repository includes a fully working Sitefinity module. It demonstrates how you can:
* Leverage the Sitefinity modular architecture to encapsulate your custom logic within a self-installable module
* Build a module with minimum implementation effort (no Manager, Provider COnfoigurations, or Permissions implementation required by this sample)
* Add a backend page and an MVC widget on it
* Add a dashboard widget

## Installation
Installing the Database diagnostic module is as easy as dropping the */DBDiagnosticsTool/bin/Debug/DBDiagnostics.dll* file in your Sitefinity website's */bin* folder. The module is self-installable thanks to the **SitefinityModuleAttribute** defined in the project's *Assemplyinfo.cs* file. For more information about **SitefinityModuleAttribute** see [Implement custom modules](https://www.progress.com/documentation/sitefinity-cms/overview-custom-modules#implement-custom-nbsp-modules)
Alternatively, you can add the DB Diagnostics module project to your Sitefintiy website's solution and add a reference to it. This path is useful in case you plan on making adjustments to the module code as well.

## Using the module
Once you start your Sitefinity website and the module is automatically installed, users in the Administrators role will see the DB Diagnostics summary widget on the Sitefintiy dashboard:
![alt text](https://github.com/Sitefinity/Database-diagnostic-module/blob/master/Screenshots/Dashboard%20widget.PNG)

The dashboard widget provides a summary of the database size, space distribution and lists the top 5 largest tables. To run full reports on the database and tables size, analyze indexes fragmentation, and rebuild database indexes use the *Go to DB diagnostics* link at the bottom of the dashbopard widget, or alternatively navigate to newly created item in the Sitefintiy backend navigation menu *Administration -> DB Diagnostics*

You will be presented with the full UI of the DB diagnostics module:
![alt text](https://github.com/Sitefinity/Database-diagnostic-module/blob/master/Screenshots/Module%20UI.PNG)
