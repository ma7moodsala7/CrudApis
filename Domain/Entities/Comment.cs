using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Comment : IAuditableEntity
    {
        public Guid CommentId { get; set; }
        public string Body { get; set; }
        public Guid RequestId { get; set; }

        [ForeignKey("User")]
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public AppUser User { get; set; }
        public Request Request { get; set; }

    }
}