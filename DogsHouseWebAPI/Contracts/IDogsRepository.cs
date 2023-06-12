using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface IDogsRepository : IRepositoryBase<Dog>
    {
        Task<PagedList<Dog>> GetDogs(DogParameters dogParameters);
        Task<Dog> GetDogById(int dogId);
        Task<Dog> GetDogByName(string dogName);
        Task CreateDog(Dog dog);
        Task UpdateDog(Dog dbDog, Dog dog);
        Task DeleteDog(Dog dog);
    }
}
