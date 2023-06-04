using Contracts;
using Entities.EF;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class DogsRepository : RepositoryBase<Dog>, IDogsRepository
    {
        public DogsRepository(DogsHouseContext repositoryContext)
                : base(repositoryContext)
        {
        }

        public PagedList<Dog> GetDogs(DogParameters dogParameters)
        {
            return PagedList<Dog>.ToPagedList(FindAll(),
                dogParameters.PageNumber,
                dogParameters.PageSize);
        }

        public Dog GetDogById(int dogId)
        {
            return FindByCondition(dog => dog.Id.Equals(dogId))
                .DefaultIfEmpty(new Dog())
                .FirstOrDefault();
        }

        public void CreateDog(Dog dog)
        {
            Create(dog);
        }

        public void UpdateDog(Dog dbDog, Dog dog)
        {
            dbDog.Map(dog);
            Update(dbDog);
        }

        public void DeleteDog(Dog dog)
        {
            Delete(dog);
        }
    }
}
