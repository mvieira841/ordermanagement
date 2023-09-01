using OrderManagement.Application.Features.Products.Enums;

namespace OrderManagement.Application.Features.Products.Queries.GetProducts;

/// <summary>
/// Get Products Query
/// </summary>
public class GetProductsQuery : IRequest<Response<PaginatedList<ProductDetails>>>
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// Start Price
    /// </summary>
    public double? StartPrice { get; set; }

    /// <summary>
    /// End Price
    /// </summary>
    [JsonPropertyName("endPrice")]
    public double? EndPrice { get; set; }

    private const int _maxPageSize = 50;

    private int _page = 10;

    /// <summary>
    /// Page Number
    /// </summary>
    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Page Size
    /// </summary>
    [JsonPropertyName("pageSize")]
    public int PageSize
    {
        get
        {
            return _page;
        }
        set
        {
            if (value > _maxPageSize) _page = _maxPageSize;
            else _page = value;
        }
    }

    /// <summary>
    /// Field
    /// </summary>
    public ProductField? Field { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    public OrderDirectionType Direction { get; set; }
}