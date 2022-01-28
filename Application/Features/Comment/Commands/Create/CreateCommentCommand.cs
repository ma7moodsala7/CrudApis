using System;
using MediatR;

namespace Application.Features.Comment.Commands.Create
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public Guid RequestId { get; set; }
        public string Body { get; set; }
        public Guid UserGuid { get; set; }
    }
}
