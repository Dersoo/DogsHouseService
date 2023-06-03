using DogsHouseWebAPI.EF;
using DogsHouseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DogsHouseWebAPI.DataInitialization
{
    public static class DataInitializer
    {
        public static void InitializeData(DogsHouseContext context)
        {
            var dogs = new List<Dog>
            {
                new Dog
                {
                    Name = "Neo",
                    Color = "red & amber",
                    TailLength = 22,
                    Weight = 32
                },
                new Dog
                {
                    Name = "Jessy",
                    Color = "black & white",
                    TailLength = 7,
                    Weight = 14
                }
            };
            dogs.ForEach(u => context.Dogs.Add(u));
            context.SaveChanges();
        }

        public static void RecreateDatabase(DogsHouseContext context)
        {
            ExecuteDeleteSql(context, "Dogs");
            ResetIdentity(context, "Dogs");
        }

        private static void ExecuteDeleteSql(DogsHouseContext context, string tableName)
        {
            var sql = $"DELETE FROM dbo.{tableName}";

            context.Database.ExecuteSqlRaw(sql);
        }

        private static void ResetIdentity(DogsHouseContext context, string tableName)
        {
            var sql = $"DBCC CHECKIDENT (\"dbo.{tableName}\", RESEED, -1);";

            context.Database.ExecuteSqlRaw(sql);
        }
    }
}
