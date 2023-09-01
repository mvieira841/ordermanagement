namespace OrderManagement.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetOrderByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<OrderDetails>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<OrderDetails>();
        var order = await _unitOfWork.OrderRepository.GetById(request.Id);

        if (order is not null)
        {
            response.Data = new OrderDetails(order, order.Customer, order.Product);
        }
        else
            response.AddErrorMessage("Order not found");

        return response;
    }
}