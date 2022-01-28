using System;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Request> GetRequestDetailsAsync(Guid id)
        {
            return DbContext.Requests
                .Include(r => r.Comments)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
