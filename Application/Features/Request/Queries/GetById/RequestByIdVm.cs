﻿using System;
using System.Collections.Generic;

namespace Application.Features.Request.Queries.GetById
{
    public class RequestByIdVm
    {
        public string Subject { get; set; }
        public string Details { get; set; }

        public string Username { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public List<RequestCommentDto> Comments { get; set; }
    }
}