using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Features.Request.Queries.GetById
{
    public class RequestCommentDto
    {
        public Guid CommentId { get; set; }
        public string Body { get; set; }

        public string Username { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}