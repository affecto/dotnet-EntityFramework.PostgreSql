using System.Configuration;
using Affecto.Configuration.Extensions;

namespace Affecto.EntityFramework.PostgreSql
{
    public class PostgreSqlConfiguration : ConfigurationSection, IPostgreSqlConfiguration
    {
        private static readonly PostgreSqlConfiguration SettingsInstance = ConfigurationManager.GetSection("postgreSql") as PostgreSqlConfiguration;

        public static PostgreSqlConfiguration Settings
        {
            get { return SettingsInstance; }
        }

        public IPostgreSqlSchemas Schemas
        {
            get { return new PostgreSqlSchemaConfiguration(SchemasInternal); }
        }

        [ConfigurationProperty("schemas", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ConfigurationElementCollection<PostgreSqlSchemaConfigurationElement>), AddItemName = "schema")]
        private ConfigurationElementCollection<PostgreSqlSchemaConfigurationElement> SchemasInternal
        {
            get { return (ConfigurationElementCollection<PostgreSqlSchemaConfigurationElement>) base["schemas"]; }
        }
    }
}