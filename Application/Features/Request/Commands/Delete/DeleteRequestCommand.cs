using System;
using MediatR;

namespace Application.Features.Request.Commands.Delete
{
    public class DeleteRequestCommand : IRequest
    {
        public DeleteRequestCommand(Guid userId, Guid requestId)
        {
            UserId = userId;
            RequestId = requestId;
        }
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
    }
}