using Entities.Models;

namespace Entities.Extensions
{
    public static class DogExtensions
    {
        public static void Map(this Dog dbDog, Dog dog)
        {
            dbDog.Name = dog.Name;
            dbDog.Color = dog.Color;
            dbDog.TailLength = dog.TailLength;
            dbDog.Weight = dog.Weight;
        }
    }
}
