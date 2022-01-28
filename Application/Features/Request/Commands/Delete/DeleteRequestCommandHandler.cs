using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Request.Commands.Delete
{
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand>
    {
        private readonly IRequestRepository _requestRepository;

        public DeleteRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<Unit> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            var requestToDelete = await _requestRepository.GetByIdAsync(request.RequestId)
                .ConfigureAwait(false);

            if (requestToDelete == null)
                throw new NotFoundException(nameof(Domain.Entities.Request), request.RequestId);

            if (requestToDelete.User.Id != request.UserId)
                throw new BadRequestException("You can only delete your own requests");

            await _requestRepository.DeleteAsync(requestToDelete).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
