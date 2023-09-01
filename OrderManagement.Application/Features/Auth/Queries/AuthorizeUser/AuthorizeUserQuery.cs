namespace OrderManagement.Application.Features.Auth.Queries.AuthorizeUser;

/// <summary>
/// Authorize User Query
/// </summary>
public class AuthorizeUserQuery : IRequest<Response<AuthorizeUserResponse>>
{
    /// <summary>
    /// User Name
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// Password
    /// </summary>
    public string? Password { get; init; }
}