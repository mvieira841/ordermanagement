namespace OrderManagement.Shared.Settings;

/// <summary>
/// Jwt Settings
/// </summary>
public class JwtSettings
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Key { get; set; }
    public int ExpiresIn { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}