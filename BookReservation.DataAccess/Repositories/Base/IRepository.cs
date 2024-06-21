using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookReservation.DataAccess.Repositories.Base
{
    /// <summary>
    /// Интерфейс для представления типизированного репозитория
    /// </summary>
    /// <typeparam name="T"></typeparam> 
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query { get; }

        IQueryable<T> UntrackedQuery { get; }

        Task<T> GetAsync(int id);

        Task<T> SaveAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
