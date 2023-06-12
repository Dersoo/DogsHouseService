using Contracts;
using Entities.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DogsHouseContext RepositoryContext { get; set; }

        public RepositoryBase(DogsHouseContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await this.RepositoryContext.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>()
                .AsNoTracking()
                .Where(expression)
                .ToListAsync();
        }

        public async Task Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
            await this.RepositoryContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
            await this.RepositoryContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
            await this.RepositoryContext.SaveChangesAsync();
        }
    }
}
