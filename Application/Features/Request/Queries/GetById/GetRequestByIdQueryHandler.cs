using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Request.Queries.GetById
{
    public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, RequestByIdVm>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetRequestByIdQueryHandler(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<RequestByIdVm> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
        {

            var requestById = await _requestRepository.GetRequestDetailsAsync(request.Id).ConfigureAwait(false);

            if (requestById == null)
                throw new NotFoundException(nameof(Domain.Entities.Request), request.Id);

            return _mapper.Map<RequestByIdVm>(requestById);
        }
    }
}
