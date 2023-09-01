namespace OrderManagement.Application.Features.Customers.Models;

/// <summary>
/// Customer Details
/// </summary>
public class CustomerDetails
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// First Name
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// Last Name
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// Contact
    /// </summary>
    public string? Contact { get; set; }
    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }
}