using System;
using MediatR;

namespace Application.Features.Request.Queries.GetByUser
{
    public class GetRequestsByUserQuery : IRequest<UserRequestsVm>
    {
        public GetRequestsByUserQuery(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; set; }
    }
}
