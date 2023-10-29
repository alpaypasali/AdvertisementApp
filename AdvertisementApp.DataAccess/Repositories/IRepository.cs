using AdvertisementApp.Common.Enums;
using AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync<Tkey>(Expression<Func<T, Tkey>> selector, OrderByType orderByType = OrderByType.DESC);
        Task<List<T>> GetAllAsync<Tkey>(Expression<Func<T, bool>> filter, Expression<Func<T, Tkey>> selector, OrderByType orderByType = OrderByType.DESC);
        Task<T> Find(object id);
        Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false);
        IQueryable<T> GetQuery();
        void Remove(T entity);
        Task CreateAsync(T entity);
        void Uptade(T entity, T unchanged);
    }
}
