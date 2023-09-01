namespace OrderManagement.API.Controllers;

/// <summary>
/// Base Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMediator? mediator;
    protected IMediator? Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    public IActionResult ReturnResponse<T>(Response<T> response)
    {
        return response.Succeeded ? Ok(response) : BadRequest(response);
    }
}