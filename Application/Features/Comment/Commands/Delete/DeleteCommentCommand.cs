using System;
using MediatR;

namespace Application.Features.Comment.Commands.Delete
{
    public class DeleteCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }

    }
}
