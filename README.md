# Temporal Implementation with EFCore 6

To be updated

## Important

1. EF Core Temporal is built on top of SQL Server [Temporal Features](https://docs.microsoft.com/en-us/sql/relational-databases/tables/temporal-tables?view=sql-server-ver15) in SQL Server 2019.  Since this is database dependent feature, this is works for EFCore packages for **MsSQL** database. **Features NOT AVAILABLE**  in other (postgres, mysql) libraries.



## Setup

1. Change the path of database file to your storage in appsettings.json



## Commands

Go to `HRMS.Dal.Migrations.MsSql` directory and following commands can be executed.

#### Add Migration

`dotnet ef  migrations add "<MIGRATION_NAME>" --startup-project ..\HRMS.API\HRMS.API.csproj`

#### Update Database

`dotnet ef   --startup-project ..\HRMS.API\HRMS.API.csproj database update`

