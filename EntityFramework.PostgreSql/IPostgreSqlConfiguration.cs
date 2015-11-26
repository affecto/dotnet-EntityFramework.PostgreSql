namespace Affecto.EntityFramework.PostgreSql
{
    public interface IPostgreSqlConfiguration
    {
        IPostgreSqlSchemas Schemas { get; }
    }
}