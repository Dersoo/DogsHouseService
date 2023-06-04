using DogsHouseWebAPI.EF.Configuration;
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
                    @"server=DESKTOP-EGV2TP5\\SQLEXPRESS;database=DogsHouse;
                      integrated security=True;TrustServerCertificate=True; MultipleActiveResultSets=True;
                      App=EntityFramework;";
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                    .ConfigureWarnings(warning =>
                        warning.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
            }
        }

        public DbSet<Dog> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DogsConfiguration());
        }

        public string GetTableName(Type type)
        {
            return Model.FindEntityType(type).GetSchemaQualifiedTableName();
        }
    }
}
