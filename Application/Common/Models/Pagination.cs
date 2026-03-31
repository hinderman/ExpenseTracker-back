namespace Application.Common.Models
{
    public sealed record Pagination<TList>(IReadOnlyList<TList> Items, int TotalCount, int PageNumber, int PageSize)
    {
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
