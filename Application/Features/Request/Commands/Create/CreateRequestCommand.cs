using System;
using MediatR;

namespace Application.Features.Request.Commands.Create
{
    public class CreateRequestCommand : IRequest<Guid>
    {
        public CreateRequestCommand(Guid userId, CreateRequestDto createRequestDto)
        {
            CreateRequestDto = createRequestDto;
            UserId = userId;
        }
        public CreateRequestDto CreateRequestDto { get; set; }
        public Guid UserId { get; set; }
    }
}
