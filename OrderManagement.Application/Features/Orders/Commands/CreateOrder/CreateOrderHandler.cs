namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Response<OrderDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<OrderDetails>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<OrderDetails>();
        var product = await _unitOfWork.ProductRepository.GetById(request.ProductId.Value);

        if(product is null)
        {
            response.AddErrorMessage("Product not found");
            return response;
        }

        var customer = await _unitOfWork.CustomerRepository.GetById(request.CustomerId.Value);

        if (customer is null)
        {
            response.AddErrorMessage("Customer not found");
            return response;
        }

        var order = new Order
        {
            CustomerId = customer.Id,
            ProductId = product.Id,
            Quantity = request.Quantity.Value
        };

        await _unitOfWork.OrderRepository.Add(order);

        response.Data = new OrderDetails(order, customer, product);

        return response;
    }
}