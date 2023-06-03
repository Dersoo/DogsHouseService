using DogsHouseWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DogsHouseWebAPI.EF
{
    public class DogsHouseContext : DbContext
    {
        public DogsHouseContext(DbContextOptions options) : base(options)
        {

        }

        public DogsHouseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString =
                    @"server=(LocalDb)\MSSQLLocalDB;database=DogsHouse;
                      integrated security=True;TrustServerCertificate=True; MultipleActiveResultSets=True;
                      App=EntityFramework;";
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                    .ConfigureWarnings(warning =>
                        warning.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
            }
        }

        public DbSet<Dog> Dog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public string GetTableName(Type type)
        {
            return Model.FindEntityType(type).GetSchemaQualifiedTableName();
        }
    }
}
