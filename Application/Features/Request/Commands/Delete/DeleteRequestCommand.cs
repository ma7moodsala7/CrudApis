using System;
using MediatR;

namespace Application.Features.Request.Commands.Delete
{
    public class DeleteRequestCommand : IRequest
    {
        public DeleteRequestCommand(Guid id)
        {
            RequestId = id;
        }
        public Guid RequestId { get; set; }
    }
}