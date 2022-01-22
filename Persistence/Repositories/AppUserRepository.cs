using System;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AppUser> GetUserByNameAsync(string username)
        {
            return DbContext.Users.FirstOrDefaultAsync(u => u.Username == username.ToLower());
        }

        public Task<AppUser> GetUserRequestsAsync(Guid userId)
        {
            return DbContext.Users.Include(p => p.Requests)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
