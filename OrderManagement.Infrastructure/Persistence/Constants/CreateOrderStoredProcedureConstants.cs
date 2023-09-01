namespace OrderManagement.Infrastructure.Persistence.Constants;

public static class CreateOrderStoredProcedureConstants
{
    public const string StoredProcedureName = "CreateOrder";
    public const string CustomerId = "@CustomerId";
    public const string ProductId = "@ProductId";
    public const string Quantity = "@Quantity";
    public const string Id = "@Id";
    public const string TotalCost = "@TotalCost";
    public const string Command = $"EXEC dbo.{StoredProcedureName} {CustomerId} = {CustomerId}, {ProductId} = {ProductId}, {Quantity} = {Quantity}, {Id} = {Id} OUT, {TotalCost} = {TotalCost} OUT";
    public const string Command2 = $"dbo.{StoredProcedureName} {CustomerId} = {CustomerId}, {ProductId} = {ProductId}, {Quantity} = {Quantity}, {Id} = {Id} OUT, {TotalCost} = {TotalCost} OUT";
}