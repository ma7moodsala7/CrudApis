using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Request.Queries.GetByUser
{
    public class GetRequestsByUserQueryHandler : IRequestHandler<GetRequestsByUserQuery, UserRequestsVm>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetRequestsByUserQueryHandler(IAppUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserRequestsVm> Handle(GetRequestsByUserQuery request, CancellationToken cancellationToken)
        {
            var userRequests = await _userRepository.GetUserRequestsAsync(request.UserId).ConfigureAwait(false);

            if (userRequests == null)
                throw new NotFoundException(nameof(Domain.Entities.AppUser), request.UserId);

            return _mapper.Map<UserRequestsVm>(userRequests);
        }
    }
}
