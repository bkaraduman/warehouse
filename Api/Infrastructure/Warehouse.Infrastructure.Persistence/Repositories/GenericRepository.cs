using Warehouse.Api.Domain.Models;
using Warehouse.Infrastructure.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Warehouse.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext dbContext;

        protected DbSet<T> entity => dbContext.Set<T>();
        public IQueryable<T> AsQueryable() => entity.AsQueryable();

        public GenericRepository(DbContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<int> AddAsync(T entity)
        {
            await this.entity.AddAsync(entity);

            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = this.entity.Find(id);

            return await DeleteAsync(entity);
        }
        public virtual async Task<int> DeleteAsync(T entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> predicate, bool noTracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }        
        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;

        }

    }
}