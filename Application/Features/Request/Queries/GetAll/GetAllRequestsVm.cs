using System.Collections.Generic;
using Domain.Common;

namespace Application.Features.Request.Queries.GetAll
{
    public class GetAllRequestsVm : IPagedEntity
    {
        public int TotalCount { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }

        public List<RequestListDto> Requests { get; set; }
    }
}