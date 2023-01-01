namespace BHS.API.ViewModels;

public class PaginatedItemsViewModel<TEntity> where TEntity : class
{
    public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    {
        PageIndex = pageIndex + 1;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public int PageIndex { get; }

    public int PageSize { get; }

    public long Count { get; }

    public IEnumerable<TEntity> Data { get; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < (double)Count / PageSize;
}