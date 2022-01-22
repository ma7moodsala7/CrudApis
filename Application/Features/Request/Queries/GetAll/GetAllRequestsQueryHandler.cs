using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Request.Queries.GetAll
{
    public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, GetAllRequestsVm>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetAllRequestsQueryHandler(IMapper mapper, IRequestRepository requestRepository)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
        }

        public async Task<GetAllRequestsVm> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            int totalRecordsCount = 0;
            IReadOnlyList<Domain.Entities.Request> requests = null;

            // get list of requests with paging feature
            if (request.PageSize != null && request.PageNo != null)
            {
                if (request.PageSize > 0 && request.PageNo > 0)
                {
                    requests = await _requestRepository.GetPagedListAsync((int) request.PageSize, (int) request.PageNo)
                        .ConfigureAwait(false);
                    totalRecordsCount = await _requestRepository.GetCountAsync().ConfigureAwait(false);
                }
            }
            else // get regular list of requests
            {
                requests = await _requestRepository.GetAllAsync().ConfigureAwait(false);
                request.PageNo = 1;
                request.PageSize = requests.Count;
                totalRecordsCount = requests.Count;
            }

            var requestListDtos = _mapper.Map<List<RequestListDto>>(requests);

            return new GetAllRequestsVm()
            {
                TotalCount = totalRecordsCount,
                PageNo = request.PageNo,
                PageSize = request.PageSize,
                Requests = requestListDtos
            };
        }
    }
}
