using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Request.Commands.Create
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, Guid>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public CreateRequestCommandHandler(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateRequestCommandValidator().ValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var newRequest = _mapper.Map<Domain.Entities.Request>(request.CreateRequestDto);
            var addAsync = await _requestRepository.AddAsync(newRequest, request.UserId).ConfigureAwait(false); 
            return addAsync.Id;
        }
    }
}
