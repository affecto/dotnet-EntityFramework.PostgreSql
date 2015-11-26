namespace Affecto.EntityFramework.PostgreSql
{
    public interface IPostgreSqlSchemas
    {
        string this[string key] { get; }
    }
}