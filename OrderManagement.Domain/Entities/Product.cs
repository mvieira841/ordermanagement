namespace OrderManagement.Domain.Entities;

/// <summary>
/// Product
/// </summary>
public class Product : Entity
{
    #region Properties
    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    public ICollection<Order>? Orders { get; set; }
    #endregion
}