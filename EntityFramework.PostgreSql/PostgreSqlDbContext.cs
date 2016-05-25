using System;
using System.Data.Entity;

namespace Affecto.EntityFramework.PostgreSql
{
    public abstract class PostgreSqlDbContext : DbContext
    {
        private readonly string defaultSchema;
        private readonly bool useLowerCasePropertyNames;

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames)
        {
            if (string.IsNullOrWhiteSpace(defaultSchema))
            {
                throw new ArgumentNullException("defaultSchema");
            }
            this.defaultSchema = defaultSchema;
            this.useLowerCasePropertyNames = useLowerCasePropertyNames;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(defaultSchema);

            if (useLowerCasePropertyNames)
            {
                modelBuilder.Conventions.Add(new LowerCasePropertyNameConvention());
                modelBuilder.Types().Configure(configuration =>
                {
                    string name = configuration.ClrType.Name.ToLower();
                    configuration.ToTable(name);
                });

                modelBuilder.Properties().Configure(configuration =>
                {
                    string name = configuration.ClrPropertyInfo.Name.ToLower();
                    configuration.HasColumnName(name);
                });
            }
        }
    }
}
