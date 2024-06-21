using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using BookReservation.DataAccess.Repositories.Base;
using BookReservation.DataAccess.Models;
using BookReservation.DataAccess;

namespace Loading.DataAccess.Repositories
{
    public class EntityRepository<T> : Repository<T> where T : class, IEntity
    {
        public EntityRepository(BaseDbContext context)
            : base(context)
        {
        }

        #region IRepository
        protected override IQueryable<T> QueryCore() => DbSet;

        protected override IQueryable<T> UntrackedQueryCore() => DbSet.AsNoTracking<T>();

        protected override async Task<T> GetCoreAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        protected override async Task<T> SaveCoreAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        protected override async Task UpdateCoreAsync(T entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        protected override async Task DeleteCoreAsync(T entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        #endregion IRepository
    }
}