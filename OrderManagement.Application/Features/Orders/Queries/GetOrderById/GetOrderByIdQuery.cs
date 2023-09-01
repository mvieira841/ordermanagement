namespace OrderManagement.Application.Features.Orders.Queries.GetOrderById;

/// <summary>
/// Get Order By Id Query
/// </summary>
public class GetOrderByIdQuery : IRequest<Response<OrderDetails>>
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
}