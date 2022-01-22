using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Request.Commands.Update
{
    public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public UpdateRequestCommandHandler(IMapper mapper, IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await new UpdateRequestCommandValidator().ValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);


            var requestToUpdate = await _requestRepository.GetByIdAsync(request.RequestId)
                .ConfigureAwait(false);
            if (requestToUpdate == null)
                throw new NotFoundException(nameof(Domain.Entities.Request), request.RequestId);

            //check if the updated request related to the same user who created it.
            if (requestToUpdate.CreatedBy != request.UserId)
                throw new BadRequestException("only the creator of this who is able to delete it.");

            requestToUpdate = _mapper.Map<Domain.Entities.Request>(request.UpdateRequestDto);

            await _requestRepository.UpdateAsync(requestToUpdate, request.UserId).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
