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
* When creating a DB context class, inherit from PostgreSqlDbContext instead of DbContext which will set the default schema and conditionally also use a lower case property names convention which is typical in PostgreSql databases. Remember to call the base class method when overriding OnModelCreating to use the given schema and naming conventions.
* When configuring to use DB migrations, inherit from PostgreSqlDbMigrationsConfiguration instead of DbMigrationsConfiguration. E.g.:
```c#
    internal sealed class Configuration : PostgreSqlDbMigrationsConfiguration<StoreContext>
    {
        public Configuration()
            : base(PostgreSqlConfiguration.Settings.Schemas["organizationregister"])
        {
        }
    }
```
* When creating DB migrations, inherit from PostgreSqlDbMigration instead of DbMigration. The class forces you to explicitly define the schema and uses the schema in all DbMigration class methods. You can also create your own migration base class where the schema is defined once for all migrations and inherit the concrete migrations from that. E.g.:
```c#
    public abstract class OrganizationRegisterDbMigration : PostgreSqlDbMigration
    {
        protected override string ResolveSchemaName()
        {
            return PostgreSqlConfiguration.Settings.Schemas["organizationregister"];
        }
    }
```

When you create new migrations, foreign keys are often created in such a way that doesn't work with PostgreSql.
A working foreign key needs to be created separately from the table creation, unlike the primary key. E.g.
```c#
    CreateTable(
        "address",
        c => new
            {
                id = c.Guid(nullable: false),
                postalcode = c.String(),
                postofficebox = c.String(),
            })
        .PrimaryKey(t => t.id);
            
    CreateTable(
        "addresslanguagespecification",
        c => new
            {
                addressid = c.Guid(nullable: false),
                languageid = c.Guid(nullable: false),
                streetaddress = c.String(),
                postaldistrict = c.String(),
                qualifier = c.String(),
            })
        .PrimaryKey(t => new { t.addressid, t.languageid })
        .Index(t => t.addressid)
        .Index(t => t.languageid);
    AddForeignKey("addresslanguagespecification", "addressid", "address", "id");
```

Do not use the EF database initializer to automatically migrate the database to the latest version.
Also do not enable the creation of automatic migrations.
Instead you need to use EF's migrate.exe during deployment to update the database to the latest version.

## Build status

| Target | Build |
| -----------------------|------------------|
| Project | [![Build status](https://ci.appveyor.com/api/projects/status/lktjrd2gg6jxka31?svg=true)](https://ci.appveyor.com/project/affecto/dotnet-entityframework-postgresql) |
| Master branch | [![Build status](https://ci.appveyor.com/api/projects/status/lktjrd2gg6jxka31/branch/master?svg=true)](https://ci.appveyor.com/project/affecto/dotnet-entityframework-postgresql/branch/master) |
| Dev branch | [![Build status](https://ci.appveyor.com/api/projects/status/lktjrd2gg6jxka31/branch/dev?svg=true)](https://ci.appveyor.com/project/affecto/dotnet-entityframework-postgresql/branch/dev) |
