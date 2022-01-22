using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly AppDbContext DbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await DbContext.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<T>> GetPagedListAsync(int pageSize, int pageNo)
        {
            return await DbContext.Set<T>()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task<T> AddAsync(T entity, Guid userGuid)
        {
            
            await DbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
            await DbContext.SaveChangesAsync(userGuid).ConfigureAwait(false);
            
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return DbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity, Guid userGuid)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return DbContext.SaveChangesAsync(userGuid);
        }

        public Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return DbContext.SaveChangesAsync();
        }

        public Task<int> GetCountAsync()
        {
            return DbContext.Set<T>().CountAsync();
        }
    }
}
