namespace Affecto.EntityFramework.PostgreSql.Configuration
{
    public interface IPostgreSqlConfiguration
    {
        IPostgreSqlSchemas Schemas { get; }
    }
}