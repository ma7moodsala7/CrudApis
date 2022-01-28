using System;
using System.Collections.Generic;
using MediatR;

namespace Application.Features.Comment.Commands.Update
{
    public class UpdateCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }
        public Guid UserGuid { get; set; }

        public string Body { get; set; }
    }
}
