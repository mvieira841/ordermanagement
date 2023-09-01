#region Using
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace OrderManagement.Domain.Common;
/// <summary>
/// Base Entity
/// </summary>
public abstract class Entity
{
    #region Properties
    /// <summary>
    /// Identity
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    #endregion
}