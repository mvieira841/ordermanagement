using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<bool>>
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    public RegisterUserHandler(IUserAuthenticationService userAuthenticationService)
    {
        _userAuthenticationService = userAuthenticationService;
    }

    public async Task<Response<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>();
        var validRoles = new List<UserRole> { UserRole.Administrator, UserRole.Manager };

        if(!validRoles.Any(r => r == request.Role))
        {
            response.AddErrorMessage("Invalid role");
            return response;
        }

        UserRegistration userForRegistration = new UserRegistration
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.UserName,
            UserName = request.UserName,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role
        };
        var userResult = await _userAuthenticationService.RegisterUserAsync(userForRegistration);

        if (!userResult.Succeeded)
            response.AddErrorMessages(userResult.Errors.Select(e => e.Description).ToList());

        response.Data = userResult.Succeeded;

        return response;
    }
}