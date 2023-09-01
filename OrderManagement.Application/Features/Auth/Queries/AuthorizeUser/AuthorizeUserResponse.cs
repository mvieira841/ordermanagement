namespace OrderManagement.Application.Features.Auth.Queries.AuthorizeUser;

/// <summary>
/// Authorize User Response
/// </summary>
public class AuthorizeUserResponse
{
    /// <summary>
    /// Access Token
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }
}