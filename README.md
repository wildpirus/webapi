# webapi for The Guardians of the Globe

Reto Sophos

## Details of configuration:

* LENGUAGE_BACKEND: C#
* FRAMEWORK_BACKEND: EntityFramework 6.0.10
* IDE: VS Code
* BASE_DE_DATOS: PostgreSql or SQL Server 2019
* GIT: git version 2.37.1.windows.1 (Used version)

Documentation on: endpoint `/swagger/` or postman collection in the projects folder `/Doc`

Using ORM, Entity Framework, so theres no need to do a sql script to create the database; using PostgreSql or SQL Server

### If it's your first time executing this project, you must use the endpoint `/api/v1/supers/createdb` to init the database and populate it.

Install project with dotnet CLI

```bash
git clone https://github.com/wildpirus/webapi-GOG.git
dotnet restore
dotnet build
dotnet run
  
```

### Don´t forget to setup the appsetting.json file

### if you're gonna use PostgreSql set your variables in the appsetting.json file as "dbEngine" : "PostgreSQL" and set your supersPGSQL variable and if you're gonna use SQL Server set "dbEngine" : "SQLServer" and set your supersSQLSR variable.

## UML class diagram

![image](https://user-images.githubusercontent.com/51038943/203902788-ca36fda3-8dcd-414a-b899-62eec2614d72.png)
