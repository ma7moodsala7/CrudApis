using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);


        Task<T> AddAsync(T entity);

        /// <param name="userGuid">auditable object id, assigned to createdBy property</param>
        Task<T> AddAsync(T entity, Guid userGuid);


        Task UpdateAsync(T entity);

        /// <param name="userGuid">auditable object id, assigned to LastModifiedBy property</param>
        Task UpdateAsync(T entity, Guid userGuid);


        Task DeleteAsync(T entity);


        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        /// Get List of records with paging feature.
        /// </summary>
        /// <param name="page">page Number</param>
        /// <param name="size">total records count per page</param>
        Task<IReadOnlyList<T>> GetPagedListAsync(int pageSize, int pageNo = 1);

        /// <summary>
        /// returns the total number of records saved in the database for specific entity.
        /// </summary>
        Task<int> GetCountAsync();
    }
}
