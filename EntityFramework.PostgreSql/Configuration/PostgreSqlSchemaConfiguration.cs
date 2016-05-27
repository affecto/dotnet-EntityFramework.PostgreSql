using System;
using System.Linq;
using Affecto.Configuration.Extensions;

namespace Affecto.EntityFramework.PostgreSql.Configuration
{
    public class PostgreSqlSchemaConfiguration : IPostgreSqlSchemas
    {
        private readonly ConfigurationElementCollection<PostgreSqlSchemaConfigurationElement> schemas;

        public PostgreSqlSchemaConfiguration(ConfigurationElementCollection<PostgreSqlSchemaConfigurationElement> schemas)
        {
            if (schemas == null)
            {
                throw new ArgumentNullException("schemas");
            }
            this.schemas = schemas;
        }

        public string this[string key]
        {
            get
            {
                if (schemas.Count(s => s.ElementKey == key) == 1)
                {
                    return schemas[key].Value;
                }

                throw new ArgumentException(string.Format("No schemas or more than one schema configured with key '{0}'.", key));
            }
        }
    }
}