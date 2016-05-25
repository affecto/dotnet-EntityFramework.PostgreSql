# EntityFramework.PostgreSql
* **Affecto.EntityFramework.PostgreSql**
  * Toolkit for configuring Entity Framework Code First approach to work with PostgreSQL database using Npgsql.
  * NuGet: https://www.nuget.org/packages/Affecto.EntityFramework.PostgreSql

## Usage
PostgreSql requires schema information with every SQL query. 
Giving this input is made easier with this package since different schemas can be configured.
Config path configuration/postgreSql/schemas contains a collection of schema elements.
Configured schemas can be accessed using the interface IPostgreSqlSchemas.

The following changes need to be made to the normal EF inheritance hierarchies:
* When creating a DB context class, inherit from PostgreSqlDbContext instead of DbContext which will set the default schema and conditionally also use a lower case property names convention which is typical in PostgreSql databases.
* When configuring to use DB migrations, inherit from PostgreSqlDbMigrationsConfiguration instead of DbMigrationsConfiguration.
* When creating DB migrations, inherit from PostgreSqlDbMigration instead of DbMigration. The class forces you to explicitly define the schema and uses the schema in all DbMigration class methods.

Do not use the EF database initializer to automatically migrate the database to the latest version.
Also do not enable the creation of automatic migrations.
Instead you need to use EF's migrate.exe during deployment to update the database to the latest version.

## Build status

| Target | Build |
| -----------------------|------------------|
| Project | [![Build status](https://ci.appveyor.com/api/projects/status/lktjrd2gg6jxka31?svg=true)](https://ci.appveyor.com/project/affecto/dotnet-entityframework-postgresql) |
| Master branch | [![Build status](https://ci.appveyor.com/api/projects/status/lktjrd2gg6jxka31/branch/master?svg=true)](https://ci.appveyor.com/project/affecto/dotnet-entityframework-postgresql/branch/master) |
| Dev branch | [![Build status](https://ci.appveyor.com/api/projects/status/lktjrd2gg6jxka31/branch/dev?svg=true)](https://ci.appveyor.com/project/affecto/dotnet-entityframework-postgresql/branch/dev) |
