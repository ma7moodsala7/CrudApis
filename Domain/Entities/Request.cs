﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Request : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }

        [ForeignKey("User")]
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public AppUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
