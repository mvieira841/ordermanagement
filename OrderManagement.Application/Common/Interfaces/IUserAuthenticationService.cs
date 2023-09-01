using Microsoft.AspNetCore.Identity;

namespace OrderManagement.Application.Common.Interfaces;

public interface IUserAuthenticationService
{
    Task<IdentityResult> RegisterUserAsync(UserRegistration userForRegistration);
    Task<bool> ValidateUserAsync(UserLogin userLogin);
    Task<string> CreateTokenAsync();
}