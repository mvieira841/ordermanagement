namespace OrderManagement.API.Controllers;

/// <summary>
/// Products Controller
/// </summary>
public class ProductsController : BaseController
{
    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Response</returns>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Response<ProductDetails>), 200)]
    [ProducesResponseType(typeof(Response<ProductDetails>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery { Id = id };
        var response = await Mediator.Send(query, cancellationToken);

        return ReturnResponse(response);
    }

    /// <summary>
    /// Get products
    /// </summary>
    /// <returns>Response</returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(Response<PaginatedList<ProductDetails>>), 200)]
    [ProducesResponseType(typeof(Response<PaginatedList<ProductDetails>>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(query, cancellationToken);

        return ReturnResponse(response);
    }

    /// <summary>
    /// Creates Product
    /// </summary>
    /// <param name="command">Command</param>
    /// <returns>Response</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(Response<ProductDetails>), 200)]
    [ProducesResponseType(typeof(Response<ProductDetails>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return ReturnResponse(response);
    }

    /// <summary>
    /// Update Product
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="command">Command</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Product Details</returns>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Response<ProductDetails>), 200)]
    [ProducesResponseType(typeof(Response<ProductDetails>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await Mediator.Send(command, cancellationToken);

        return ReturnResponse(response);
    }

    /// <summary>
    /// Delete Product
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Product Details</returns>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Response<ProductDetails>), 200)]
    [ProducesResponseType(typeof(Response<ProductDetails>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
    {
        DeleteProductCommand command = new DeleteProductCommand
        {
            Id = id,
        };
        var response = await Mediator.Send(command, cancellationToken);

        return ReturnResponse(response);
    }
}