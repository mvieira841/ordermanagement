namespace OrderManagement.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response<ProductDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<ProductDetails>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<ProductDetails>();
        var product = await _unitOfWork.ProductRepository.GetById(request.Id);

        if (product is null)
        {
            response.AddErrorMessage($"Product not found");
            return response;
        }

        if(!string.Equals(request.Name, product.Name, StringComparison.InvariantCultureIgnoreCase))
        {
            var existAnotherProduct = await _unitOfWork.ProductRepository.GetOneByCondition(p => p.Name == request.Name && p.Id != product.Id);
            if(existAnotherProduct is not null)
            {
                response.AddErrorMessage($"There exists another product with the same name");
            }
        }
        
        if(response.Succeeded)
        {
            product.Name = request.Name;
            product.Price = request.Price;
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();
            response.Data = new ProductDetails(product);
        }

        return response;
    }
}