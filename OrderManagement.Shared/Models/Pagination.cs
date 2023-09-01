namespace OrderManagement.Shared.Models;

/// <summary>
/// Pagination
/// </summary>
public class Pagination
{
    const int maxPageSize = 50;
    /// <summary>
    /// Page Number
    /// </summary>
    public int PageNumber { get; set; } = 1;

    private int pagesize = 10;

    /// <summary>
    /// Page Size
    /// </summary>
    public int PageSize
    {
        get
        {
            return pagesize;
        }
        set
        {
            pagesize = (value > maxPageSize) ? pagesize : value;
        }
    }

    /// <summary>
    /// Sort
    /// </summary>
    public IEnumerable<Sort>? Sort { get; set; }
}