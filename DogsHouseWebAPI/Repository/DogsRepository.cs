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

        public PagedList<Dog> GetDogs(DogParameters dogParameters)
        {
            var sortedDogs = _sortHelper.ApplySort(FindAll(), dogParameters.OrderBy);

            return PagedList<Dog>.ToPagedList(sortedDogs,
                dogParameters.PageNumber,
                dogParameters.PageSize);
        }

        public Dog GetDogById(int dogId)
        {
            return FindByCondition(dog => dog.Id.Equals(dogId)).AsEnumerable()
                .DefaultIfEmpty(new Dog())
                .FirstOrDefault();
        }

        public Dog GetDogByName(string dogName)
        {
            return FindByCondition(dog => dog.Name.Equals(dogName)).AsEnumerable()
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
