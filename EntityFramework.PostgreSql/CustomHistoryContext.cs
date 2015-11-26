using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace Affecto.EntityFramework.PostgreSql
{
    public class CustomHistoryContext : HistoryContext
    {
        public CustomHistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable("migration_history");
            modelBuilder.Entity<HistoryRow>().Property(p => p.MigrationId).HasColumnName("migration_id");
            modelBuilder.Entity<HistoryRow>().Property(p => p.ContextKey).HasColumnName("context_key");
            modelBuilder.Entity<HistoryRow>().Property(p => p.Model).HasColumnName("model");
            modelBuilder.Entity<HistoryRow>().Property(p => p.ProductVersion).HasColumnName("product_version");
        }
    }
}