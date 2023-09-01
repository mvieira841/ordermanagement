using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Application.Common.Interfaces;
using OrderManagement.Application.Common.Models;
using OrderManagement.Shared.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderManagement.Infrastructure.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;
    private User? _user;

    public UserAuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSettings> options)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = options.Value;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserRegistration userRegistration)
    {
        var user = new User
        {
            FirstName = userRegistration.FirstName,
            LastName = userRegistration.LastName,
            Email = userRegistration.Email,
            UserName = userRegistration.UserName,
            PhoneNumber = userRegistration.PhoneNumber,
        };
        var result = await _userManager.CreateAsync(user, userRegistration.Password);

        if(result.Succeeded)
        {
            result = await _userManager.AddToRoleAsync(user, userRegistration.Role.GetDescription());
        }

        return result;
    }

    public async Task<bool> ValidateUserAsync(UserLogin userLogin)
    {
        _user = await _userManager.FindByNameAsync(userLogin.UserName);
        var result = _user != null && await _userManager.CheckPasswordAsync(_user, userLogin.Password);
        return result;
    }

    public async Task<string> CreateTokenAsync()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiresIn)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}