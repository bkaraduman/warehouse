using Warehouse.Api.Domain.Models;
using System.Linq.Expressions;

namespace Warehouse.Infrastructure.Persistence.Repositories.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> UpdateAsync(T entity);
        IQueryable<T> AsQueryable();
        Task<List<T>> GetList(Expression<Func<T, bool>> predicate, bool noTracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);
    }
}
