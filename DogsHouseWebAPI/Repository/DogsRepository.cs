using Contracts;
using Entities.EF;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Helpers.SortHelper;
using Entities.Models;

namespace Repository
{
    public class DogsRepository : RepositoryBase<Dog>, IDogsRepository
    {
        private ISortHelper<Dog> _sortHelper;

        public DogsRepository(DogsHouseContext repositoryContext, ISortHelper<Dog> sortHelper)
                : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Dog>> GetDogs(DogParameters dogParameters)
        {
            var sortedDogs = _sortHelper.ApplySort(await FindAll(), dogParameters.OrderBy);

            return PagedList<Dog>.ToPagedList(sortedDogs,
                dogParameters.PageNumber,
                dogParameters.PageSize);
        }

        public async Task<Dog> GetDogById(int dogId)
        {
            var dogs = await FindByCondition(dog => dog.Id.Equals(dogId));

            return dogs
                .DefaultIfEmpty(new Dog())
                .FirstOrDefault();
        }

        public async Task<Dog> GetDogByName(string dogName)
        {
            var dogs = await FindByCondition(dog => dog.Name.Equals(dogName));

            return dogs.FirstOrDefault();
        }

        public async Task CreateDog(Dog dog)
        {
            await Create(dog);
        }

        public async Task UpdateDog(Dog dbDog, Dog dog)
        {
            dbDog.Map(dog);
            await Update(dbDog);
        }

        public async Task DeleteDog(Dog dog)
        {
            await Delete(dog);
        }
    }
}
