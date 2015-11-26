using System.Data.Entity;

namespace Affecto.EntityFramework.PostgreSql
{
    public abstract class HistoryContextConfiguration : DbConfiguration
    {
        protected HistoryContextConfiguration(string schemaName)
        {
            SetHistoryContext("Npgsql", (connection, defaultSchema) => new CustomHistoryContext(connection, schemaName));
        }
    }
}