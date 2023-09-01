namespace OrderManagement.Application.Features.Products.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Response<ProductDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<ProductDetails>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<ProductDetails>();
        var existingProduct = await _unitOfWork.ProductRepository.GetByCondition(p => p.Name == request.Name, false);

        if (existingProduct is not null && existingProduct.Count() > 0)
        {
            response.AddErrorMessage($"The Product {request.Name} already exists");
        }
        else
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };
            await _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Save();
            response.Data = new ProductDetails(product);
        }

        return response;
    }
}