using OrderManagement.Shared.Models;

namespace OrderManagement.Application.Features.Customers.Query.GetCustomers;

/// <summary>
/// Get Customers Query
/// </summary>
public class GetCustomersQuery : IRequest<Response<PaginatedList<CustomerDetails>>>
{
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

    /// <summary>
    /// Pagination
    /// </summary>
    public Pagination Pagination { get; set; } = new Pagination();
}