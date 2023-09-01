namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder;

/// <summary>
/// Create Order Command
/// </summary>
public class CreateOrderCommand : IRequest<Response<OrderDetails>>
{
    /// <summary>
    /// Customer Id
    /// </summary>
    public int? CustomerId { get; set; }

    /// <summary>
    /// Product Id
    /// </summary>
    public int? ProductId { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    public int? Quantity { get; set; }
}