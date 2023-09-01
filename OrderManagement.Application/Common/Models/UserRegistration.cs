namespace OrderManagement.Application.Common.Models;

public class UserRegistration
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public UserRole Role { get; set; }
}