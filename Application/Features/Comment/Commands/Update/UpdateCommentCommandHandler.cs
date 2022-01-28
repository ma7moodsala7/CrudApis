using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Comment.Commands.Update
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;


        public UpdateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToUpdate = await _commentRepository.GetByIdAsync(request.CommentId)
                .ConfigureAwait(false);

            if (commentToUpdate.User.Id != request.UserGuid)
                throw new BadRequestException("You can only update your own comments");

            if (commentToUpdate == null)
                throw new NotFoundException(nameof(Domain.Entities.Comment), request.CommentId);

            var validator = new UpdateCommentCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            commentToUpdate.Body = request.Body;
            await _commentRepository.UpdateAsync(commentToUpdate, request.UserGuid).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
