namespace OrderManagement.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>();
        var product = await _unitOfWork.ProductRepository.GetById(request.Id);

        if (product is null)
        {
            response.AddErrorMessage($"Product not found");
        }
        else
        {
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Save();
            response.Data = true;
        }

        return response;
    }
}
