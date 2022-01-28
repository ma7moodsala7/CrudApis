using System;

namespace Application.Features.Comment.Commands.Create
{
    public class CreateCommentDto
    {
        public Guid RequestId { get; set; }
        public string Body { get; set; }
    }
}
