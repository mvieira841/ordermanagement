namespace OrderManagement.API.Controllers;

/// <summary>
/// Customers Controller
/// </summary>
public class CustomersController : BaseController
{
    /// <summary>
    /// Get Customers/Search
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Response</returns>
    [HttpPost("search")]
    [Authorize()]
    [ProducesResponseType(typeof(Response<PaginatedList<CustomerDetails>>), 200)]
    [ProducesResponseType(typeof(Response<PaginatedList<CustomerDetails>>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> GetCustomers([FromBody] GetCustomersQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}