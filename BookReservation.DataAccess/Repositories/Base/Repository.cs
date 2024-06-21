using Microsoft.EntityFrameworkCore;
using BookReservation.DataAccess.Models;

namespace BookReservation.DataAccess.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BaseDbContext Context;
        protected DbSet<T> DbSet;

        protected Repository(BaseDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        #region IRepository
        protected abstract IQueryable<T> QueryCore();

        protected abstract IQueryable<T> UntrackedQueryCore();

        protected abstract Task<T> GetCoreAsync(int id);

        protected abstract Task<T> SaveCoreAsync(T entity);

        protected abstract Task UpdateCoreAsync(T entity);

        protected abstract Task DeleteCoreAsync(T entity);

        public virtual IQueryable<T> Query => QueryCore();

        public virtual IQueryable<T> UntrackedQuery => UntrackedQueryCore();

        public async Task<T> GetAsync(int id)
        {
            return await GetCoreAsync(id);
        }

        public async Task<T> SaveAsync(T entity)
        {
            return await SaveCoreAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await UpdateCoreAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await DeleteCoreAsync(entity);
        }

        #endregion
    }
}
