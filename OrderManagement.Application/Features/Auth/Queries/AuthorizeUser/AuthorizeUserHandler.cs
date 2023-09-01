using OrderManagement.Application.Common.Interfaces;

namespace OrderManagement.Application.Features.Auth.Queries.AuthorizeUser;

public class AuthorizeUserHandler : IRequestHandler<AuthorizeUserQuery, Response<AuthorizeUserResponse>>
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    public AuthorizeUserHandler(IUserAuthenticationService userAuthenticationService)
    {
        _userAuthenticationService = userAuthenticationService;
    }

    public async Task<Response<AuthorizeUserResponse>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<AuthorizeUserResponse>();

        UserLogin userLogin = new UserLogin
        {
            UserName = request.UserName,
            Password = request.Password
        };
        var userResult = await _userAuthenticationService.ValidateUserAsync(userLogin);

        if (!userResult)
            response.AddErrorMessage("Invalid UserName and/or Password");
        else
        {
            var token = await _userAuthenticationService.CreateTokenAsync();
            response.Data = new AuthorizeUserResponse
            {
                AccessToken = token
            };
        }

        return response;
    }
}