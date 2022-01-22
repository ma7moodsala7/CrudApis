using MediatR;

namespace Application.Features.Request.Queries.GetAll
{
    public class GetAllRequestsQuery : IRequest<GetAllRequestsVm>
    {
        public GetAllRequestsQuery(int? pageSize, int? pageNo)
        {
            PageSize = pageSize;
            PageNo = pageNo;
        }
        public int? PageSize { get; set; }

        public int? PageNo { get; set; }
    }
}