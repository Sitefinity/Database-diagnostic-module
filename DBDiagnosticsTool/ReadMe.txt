Database diagnostics is a tool that reveals the most important database information to site administrator and gives the ability to execute a basic database maintenance tasks directly from Sitefinity backend. 
It displays basic information on a dashboard widget and more details and maintenance operations are shown on a specific page (Administration > DB Diagnostics).

Site administrator can check:
	Database size
	Table space usage
	Index fragmentation

The following active operations can be performed:
	Rebuild database indexes


How to use it 
	- Install nuget package in your project
	- Or place the .dll in your bin folder

FAQ: 
	Q: May other users see the database information (e.g. Content Editors)?
	A: No. It was designed to help site administrators only and it is protected.

	Q: Is this tool available for Oracle/MySQL?
	A: No. It is available for MSSQL Server 2017+ and SLQ Azure type of databases. The rest of the database servers are not supported.

	Q: May I use it on my production server? Is there a performance penalty?
	A: Yes. Read operations are executed on demand. No background operation runs sql queries. Rebuild index operation may cause a slower queries for short amount of time (e.g. 2-3 secs per query type) for lower SQL Server editions (e.g. Standard). For SQL Server Enterprise edition no delay is expected.



