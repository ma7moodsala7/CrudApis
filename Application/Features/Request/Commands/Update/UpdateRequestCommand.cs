using System;
using MediatR;

namespace Application.Features.Request.Commands.Update
{
    public class UpdateRequestCommand : IRequest<Unit>
    {
        public UpdateRequestCommand(Guid userId, Guid requestId, UpdateRequestDto updateRequestDto)
        {
            UserId = userId;
            RequestId = requestId;
            UpdateRequestDto = updateRequestDto;
        }
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
        public UpdateRequestDto UpdateRequestDto { get; set; }
    }
}