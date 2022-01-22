using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IAppUserRepository : IAsyncRepository<AppUser>
    {
        Task<AppUser> GetUserByNameAsync(string userName);
        Task<AppUser> GetUserRequestsAsync(Guid userId);
    }
}
