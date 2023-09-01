namespace OrderManagement.API.Controllers;

/// <summary>
/// Orders Controller
/// </summary>
public class OrdersController : BaseController
{
    /// <summary>
    /// Get Order by Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Response</returns>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Response<OrderDetails>), 200)]
    [ProducesResponseType(typeof(Response<OrderDetails>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery { Id = id };
        var response = await Mediator.Send(query, cancellationToken);

        return ReturnResponse(response);
    }

    /// <summary>
    /// Create Order
    /// </summary>
    /// <param name="command">Command</param>
    /// <returns>Response</returns>
    [HttpPost]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}