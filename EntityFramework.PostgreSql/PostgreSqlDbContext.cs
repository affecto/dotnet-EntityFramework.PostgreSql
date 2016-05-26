using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Affecto.EntityFramework.PostgreSql
{
    public abstract class PostgreSqlDbContext : DbContext
    {
        private string defaultSchema;
        private bool useLowerCasePropertyNames;

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames)
        {
            SetInstanceVariables(defaultSchema, useLowerCasePropertyNames);
        }

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames, DbCompiledModel model)
            : base(model)
        {
            SetInstanceVariables(defaultSchema, useLowerCasePropertyNames);
        }

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            SetInstanceVariables(defaultSchema, useLowerCasePropertyNames);
        }

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames, string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
            SetInstanceVariables(defaultSchema, useLowerCasePropertyNames);
        }

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames, DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            SetInstanceVariables(defaultSchema, useLowerCasePropertyNames);
        }

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames, DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            SetInstanceVariables(defaultSchema, useLowerCasePropertyNames);
        }

        protected PostgreSqlDbContext(string defaultSchema, bool useLowerCasePropertyNames, ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            SetInstanceVariables(defaultSchema, useLowerCasePropertyNames);
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

        private void SetInstanceVariables(string defaultSchema, bool useLowerCasePropertyNames)
        {
            this.defaultSchema = defaultSchema;
            this.useLowerCasePropertyNames = useLowerCasePropertyNames;
        }
    }
}
