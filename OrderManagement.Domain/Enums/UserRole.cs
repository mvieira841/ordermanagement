using System.ComponentModel;

namespace OrderManagement.Domain.Enums;

public enum UserRole
{
    [Description("Administrator")]
    Administrator,
    [Description("Manager")]
    Manager
}