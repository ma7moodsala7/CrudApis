using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Comment.Commands.Create
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IRequestRepository _requestRepository;




        public CreateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper, IRequestRepository requestRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _requestRepository = requestRepository;
        }

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var requestOfComment = await _requestRepository.GetRequestDetailsAsync(request.RequestId).ConfigureAwait(false);
            if (requestOfComment == null)
                throw new NotFoundException(nameof(Domain.Entities.Request), request.RequestId);

            var validator = new CreateCommentCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var newComment = _mapper.Map<Domain.Entities.Comment>(request);

            if (requestOfComment.Comments == null)
                    requestOfComment.Comments = new List<Domain.Entities.Comment>();

            requestOfComment.Comments.Add(newComment);
            newComment = await _commentRepository.AddAsync(newComment, request.UserGuid).ConfigureAwait(false);

            return newComment.CommentId;
        }
    }
}
