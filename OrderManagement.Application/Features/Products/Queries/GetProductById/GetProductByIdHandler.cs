namespace OrderManagement.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Response<ProductDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetProductByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<ProductDetails>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<ProductDetails>();
        var product = await _unitOfWork.ProductRepository.GetById(request.Id);

        if (product is not null)
        {
            response.Data = new ProductDetails(product);
        }
        else
            response.AddErrorMessage("Product not found");

        return response;
    }
}