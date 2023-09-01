namespace OrderManagement.API.Controllers;

/// <summary>
/// Auth Controller
/// </summary>
public class AuthController : BaseController
{
    private readonly JwtSettings jwtSettings;
    public AuthController(IOptions<JwtSettings> options)
    {
        jwtSettings = options.Value;
    }

    /// <summary>
    /// Register User
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(Response<IdentityResult>), 200)]
    [ProducesResponseType(typeof(Response<IdentityResult>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return ReturnResponse(result);
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="query">Authorize User</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Response</returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(Response<AuthorizeUserResponse>), 200)]
    [ProducesResponseType(typeof(Response<AuthorizeUserResponse>), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> Login([FromBody] AuthorizeUserQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        if (result.Succeeded)
            return Ok(result.Data);
        return Unauthorized((Response)result);
    }
}
