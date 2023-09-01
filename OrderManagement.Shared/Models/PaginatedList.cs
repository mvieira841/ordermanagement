namespace OrderManagement.Shared.Models;

/// <summary>
/// Paginated List
/// </summary>
/// <typeparam name="T">Type</typeparam>
public class PaginatedList<T>
{
    /// <summary>
    /// Items
    /// </summary>
    public List<T> Items { get; private set; }

    /// <summary>
    /// Page Number
    /// </summary>
    public int PageNumber { get; private set; }
    
    /// <summary>
    /// TotalPages
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// Total Count
    /// </summary>
    public int TotalCount { get; private set; }

    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        SetUp(items, count, pageNumber, pageSize);
    }

    /// <summary>
    /// Has Previous Page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Has Next Page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        SetUp(items, count, pageNumber, pageSize);
    }

    private void SetUp(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }
}