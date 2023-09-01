namespace OrderManagement.Application.Features.Products.Model;

/// <summary>
/// Product Details
/// </summary>
public class ProductDetails
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("price")]
    public double Price { get; set; }

    public ProductDetails()
    {

    }

    public ProductDetails(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
    }
}