using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.EF.Configuration
{
    public class DogsConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder.ToTable("Dogs");

            builder.HasData
            (
                new Dog
                {
                    Id = 1,
                    Name = "Neo",
                    Color = "red & amber",
                    TailLength = 22,
                    Weight = 32
                },
                new Dog
                {
                    Id=2,
                    Name = "Jessy",
                    Color = "black & white",
                    TailLength = 7,
                    Weight = 14
                }
            );
        }
    }
}
