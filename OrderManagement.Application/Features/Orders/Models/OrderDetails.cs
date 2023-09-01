namespace OrderManagement.Application.Features.Orders.Models;

/// <summary>
/// Order Details
/// </summary>
public class OrderDetails
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Customer Id
    /// </summary>
    [JsonPropertyName("customerId")]
    public int CustomerId { get; set; }

    /// <summary>
    /// Customer Name
    /// </summary>
    [JsonPropertyName("customerName")]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Product Id
    /// </summary>
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    /// <summary>
    /// Product Name
    /// </summary>
    [JsonPropertyName("productName")]
    public string? ProductName { get; set; }

    /// <summary>
    /// Product Price
    /// </summary>
    [JsonPropertyName("productPrice")]
    public double ProductPrice { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>
    /// Total Cost
    /// </summary>
    [JsonPropertyName("totalCost")]
    public double TotalCost { get; set; }
    public OrderDetails()
    {

    }
    public OrderDetails(Order order, Customer customer, Product product)
    {
        Id = order.Id;
        CustomerId = order.CustomerId;
        CustomerName = $"{customer.FirstName} {customer.LastName}";
        ProductId = order.ProductId;
        ProductName = product.Name;
        Quantity = order.Quantity;
        ProductPrice = product.Price;
        TotalCost = order.TotalCost;
    }
}