namespace OrderManagement.Domain.Entities;

public class Customer : Entity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Contact { get; set; }
    public string? Email { get; set; }
}