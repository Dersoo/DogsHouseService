using Contracts;
using Entities.EF;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DogsHouseContext _repoContext;
        private IDogsRepository _dogs;

        public IDogsRepository Dogs
        {
            get
            {
                if (_dogs == null)
                {
                    _dogs = new DogsRepository(_repoContext);
                }

                return _dogs;
            }
        }

        public RepositoryWrapper(DogsHouseContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
