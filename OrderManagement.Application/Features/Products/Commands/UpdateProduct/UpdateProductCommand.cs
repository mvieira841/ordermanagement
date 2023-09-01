namespace OrderManagement.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Update Product Command
/// </summary>
public class UpdateProductCommand : IRequest<Response<ProductDetails>>
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Price
    /// </summary>
    public double Price { get; set; }
}