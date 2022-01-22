namespace Domain.Common
{
    public interface IPagedEntity
    {
        int TotalCount { get; set; }
        int? PageNo { get; set; }
        int? PageSize { get; set; }
    }
}
