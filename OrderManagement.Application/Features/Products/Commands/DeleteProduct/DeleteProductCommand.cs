namespace OrderManagement.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Delete Product Command
/// </summary>
public class DeleteProductCommand : IRequest<Response<bool>>
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
}