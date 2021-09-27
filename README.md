# Smart EV Charging System

# .NET 5.0 | Visual Studio 2022/19

* Open project in Visual Studio 2022/2019
* Restore Nuget at solution level.
* Integrated Database is MS SQL server (Azure) with ORM Entity Core 5.0. 
* Database UserName & Password for SSMS access . (SQL Authentication Type) 
	* UN- **centraluser**
	* Pass- **Welcome@123**
	* if database access doesn't work then Application.SmartCharging.DB project can be leveraged to generate database
	* Alternatively, email your IP [daya.shankar58@gmail.com] and I will add it. Database will live by 10/10/21 9.00 PM IST. 
* Set Application.SmartCharging.Service as Startup Project
* Run the project and it should launch a browser with swagger enabled RESTful APIs of Group, ServiceStation & Connector

* default swagger URL-  https://localhost:5001/swagger/index.html

* TODO are some open areas where improvements can be done later.
