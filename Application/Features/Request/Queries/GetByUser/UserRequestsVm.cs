using System.Collections.Generic;

namespace Application.Features.Request.Queries.GetByUser
{
    public class UserRequestsVm
    {
        public string Username { get; set; }

        public List<UserRequestDto> Requests { get; set; }
    }
}