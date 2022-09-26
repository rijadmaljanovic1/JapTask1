# JapTask1

Project created for Jap. 
The project consists of two parts: Web API - backend created using ASP .Net Core 6.0 and Angular - frontend + Docker

# Requirements:
 Database:
 -SQL Server ( SQL Server , graphical tool -> Microsoft SQL Server Management Studio )
 Container:
 -Docker

## Application setup and launch

1. Clone repository <br/>

2. In the folder where the project is located, enter the following commands (CMD / Powershell): <br/>

- docker-compose build
- docker-compose up

 3. After docker starts the containers, open in browser: http://localhost:5001/swagger
 
 4. Run the angular app with the following command:
- npm install <br/>
- ng serve <br/>
*Important disclaimer - npm install is for the installation of the needed modules

If you want to start the application without docker: *NOT RECOMMENDED

You can set the connection string in appsettings.json by your requirements and in the PackageManagerConsole switch to the .Infrastructure project and write the command 'EntityFrameworkCore\Update-Database'. 

## Login credentials (token based):
```
Username: User1
Password: User1
```
...
```
Username: User5
Password: User5
```
