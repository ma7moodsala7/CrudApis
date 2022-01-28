using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IRequestRepository : IAsyncRepository<Request>
    {
        Task<Request> GetRequestDetailsAsync(Guid id);
    }
}
