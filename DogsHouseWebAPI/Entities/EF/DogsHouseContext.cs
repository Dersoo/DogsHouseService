﻿using Entities.EF.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Entities.EF
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
