using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Affecto.EntityFramework.PostgreSql
{
    public abstract class HistoryContextDbMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext> where TContext : DbContext
    {
        protected HistoryContextDbMigrationsConfiguration(string schemaName)
        {
            SetHistoryContextFactory("Npgsql", (connection, defaultSchema) => new CustomHistoryContext(connection, schemaName));
        }
    }
}