using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.Features.Auth.Commands.RegisterUser;

/// <summary>
/// Register User Command
/// </summary>
public class RegisterUserCommand : IRequest<Response<bool>>
{
    /// <summary>
    /// First Name
    /// </summary>
    public string? FirstName { get; init; }
    /// <summary>
    /// Last Name
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// User Name
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// Password
    /// </summary>
    public string? Password { get; init; }

    /// <summary>
    /// Phone Number
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Role
    /// </summary>
    public UserRole Role { get; set; }
}