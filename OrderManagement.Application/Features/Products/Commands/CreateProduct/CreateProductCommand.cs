namespace OrderManagement.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Create Product Command
/// </summary>
public class CreateProductCommand : IRequest<Response<ProductDetails>>
{
    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Price
    /// </summary>
    public double Price { get; set; }
}