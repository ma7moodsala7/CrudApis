using System;

namespace Application.Features.Request.Queries.GetByUser
{
    public class UserRequestDto
    {
        public Guid RequestId { get; set; }

        public string Subject { get; set; }

        public string Details { get; set; }
    }
}
