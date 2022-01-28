using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ICommentRepository : IAsyncRepository<Comment>
    {
    }
}
