using OrderManagement.Application.Features.Products.Enums;

namespace OrderManagement.Application.Features.Products.Queries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, Response<PaginatedList<ProductDetails>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetProductsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<PaginatedList<ProductDetails>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<PaginatedList<ProductDetails>>();

        var products = await _unitOfWork.ProductRepository.GetQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            products = products.Where(p => EF.Functions.Like(p.Name, $"%{request.Name}%"));
        }

        if (request.StartPrice.HasValue)
        {
            products = products.Where(p => p.Price >= request.StartPrice);
        }

        if (request.EndPrice.HasValue)
        {
            products = products.Where(p => p.Price <= request.EndPrice);
        }

        if (request.Field.HasValue)
        {
            if (request.Direction == OrderDirectionType.Ascending)
            {
                products = request.Field switch
                {
                    ProductField.Name => products.OrderBy(p => p.Name),
                    ProductField.Price => products.OrderBy(p => p.Price),
                    _ => products.OrderBy(p => p.Name)
                };
            }
            else
            {
                products = request.Field switch
                {
                    ProductField.Name => products.OrderByDescending(p => p.Name),
                    ProductField.Price => products.OrderByDescending(p => p.Price),
                    _ => products.OrderByDescending(p => p.Name)
                };
            }
        }
        else
            products = products.OrderBy(p => p.Name);

        var productDetails = products.Select(x => new ProductDetails(x));

        response.Data = new PaginatedList<ProductDetails>(productDetails, request.PageNumber, request.PageSize);

        return response;
    }
}