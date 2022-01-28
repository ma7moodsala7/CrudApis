using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Comment.Commands.Delete
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToDelete = await _commentRepository.GetByIdAsync(request.CommentId)
                .ConfigureAwait(false);

            if (commentToDelete == null)
                throw new NotFoundException(nameof(Domain.Entities.Request), request.CommentId);

            await _commentRepository.DeleteAsync(commentToDelete).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
