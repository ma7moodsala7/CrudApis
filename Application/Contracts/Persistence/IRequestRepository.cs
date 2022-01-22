using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IRequestRepository : IAsyncRepository<Request>
    {
    }
}
