using OrderManagement.Shared.Enums;

namespace OrderManagement.Shared.Models;

public class Sort
{
    /// <summary>
    /// Field
    /// </summary>
    public string? Field { get; set; }
    
    /// <summary>
    /// Direction
    /// </summary>
    public OrderDirectionType Direction { get; set; }
}