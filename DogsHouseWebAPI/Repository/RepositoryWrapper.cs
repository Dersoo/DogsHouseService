using Contracts;
using Entities.EF;
using Entities.Helpers.SortHelper;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DogsHouseContext _repoContext;
        private IDogsRepository _dogs;
        private ISortHelper<Dog> _dogSortHelper;

        public IDogsRepository Dogs
        {
            get
            {
                if (_dogs == null)
                {
                    _dogs = new DogsRepository(_repoContext, _dogSortHelper);
                }

                return _dogs;
            }
        }

        public RepositoryWrapper(DogsHouseContext repositoryContext, ISortHelper<Dog> dogSortHelper)
        {
            _repoContext = repositoryContext;
            _dogSortHelper = dogSortHelper;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
