using System;
using MediatR;

namespace Application.Features.Request.Queries.GetById
{
    public class GetRequestByIdQuery : IRequest<RequestByIdVm>
    {
        public GetRequestByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
