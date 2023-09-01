using OrderManagement.Application.Features.Products.Model;

namespace OrderManagement.Application.Features.Products.Queries.GetProductById;

/// <summary>
/// Get Product By Id Query
/// </summary>
public class GetProductByIdQuery : IRequest<Response<ProductDetails>>
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
}