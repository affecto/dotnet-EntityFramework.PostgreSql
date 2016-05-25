using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Affecto.EntityFramework.PostgreSql
{
    public abstract class PostgreSqlDbMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext> where TContext : DbContext
    {
        protected PostgreSqlDbMigrationsConfiguration(string schemaName)
        {
            AutomaticMigrationsEnabled = false;
            SetHistoryContextFactory("Npgsql", (connection, defaultSchema) => new CustomHistoryContext(connection, schemaName));
        }
    }
}