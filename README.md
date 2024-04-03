# CS_4320 Project 2 - iCLOTHING
CS_4320 Project 2: Implementing the iCLOTHING Application using Visual Studio
# Software Requirements:
1) Latest version of Microsoft SQL server Management Studio
2) Visual Studio 2022 Community Edition
    a) Installed components: ASP.Net and web development, .Net Desktop development, Data storage and processing
    b) Individual components: Advanced ASP.NET features, .NET Framework project and item templates,  .NET Framework 4.6 targeting pack, Visual Studio SDK, Modeling SDK, ASP.NET MVC 4, MSVC v143 - VS 2022 C++ x64/x86 build tools (Latest), .NET Framework 4.7.2 SDK
   
# Run instructions:
1) Clone repository in a folder directly in C: root.
2) Launch Microsoft SQL Server Management Studio as Administrator
     a) Connect to your local server
     b) Right click "Databases" > "Restore Databases"
     c) Under "Source" select "Device" > click "..." button and navigate to the project folder. Select the root project folder.
     d) In right window, select "Group11_iCLOTHINGDB.bak". Click Ok multiple times to confirm selection.
3) Open the visual studio project solution in Visual Studio 2022.
4) Connect to the database:
     a) Click "Tools" > "Connect to Database"
     b) Enter your local server name (by default: same as computer name)
     c) Under "Connect to a database" type "Group11_iCLOTHINGDB". Click Ok.
5) ** IMPORTANT ** Edit the "Web.config" file in the root project folder.
     a) Open "Web.config" and type CTRL + F to search for the term "data source"
     b) After the "=" sign and before the ";", type the name of your local server instead of the name given. Save the file.
6) ** IMPORTANT ** After restarting Visual Studio, Click the solid green triangle with text "IIS Express (Microsoft Edge)" to launch the application.

# Application Features
No-Login User View: Users are able to browse products all while being signed out.
Logged-In Customer View: Customers who register, can login and purchase items from the website.
Administrator View: When the administrator logs in, they have a special view to manage website data.
