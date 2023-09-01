namespace OrderManagement.Domain.Entities;

/// <summary>
/// Order
/// </summary>
public class Order : Entity
{
    #region Properties
    /// <summary>
    /// Customer Id
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Customer
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Product Id
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Product
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Total Cost
    /// </summary>
    public double TotalCost { get; set; }
    #endregion
}