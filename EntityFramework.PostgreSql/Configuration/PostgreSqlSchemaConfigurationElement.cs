using System.Configuration;
using Affecto.Configuration.Extensions;

namespace Affecto.EntityFramework.PostgreSql.Configuration
{
    public class PostgreSqlSchemaConfigurationElement : ConfigurationElementBase
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string) this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string) this["value"]; }
            set { this["value"] = value; }
        }

        public override string ElementKey
        {
            get { return Key; }
        }

        protected override void PostDeserialize()
        {
            base.PostDeserialize();

            if (string.IsNullOrWhiteSpace(Key))
            {
                throw new ConfigurationErrorsException("PostgreSQL schema key must be set.");
            }
        }
    }
}