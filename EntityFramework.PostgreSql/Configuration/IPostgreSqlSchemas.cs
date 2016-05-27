namespace Affecto.EntityFramework.PostgreSql.Configuration
{
    public interface IPostgreSqlSchemas
    {
        string this[string key] { get; }
    }
}