using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Infrastructure.Persistence.Context;
using System;
using System.Data;

namespace OrderManagement.Test.Tests;

public class DatabaseAccessTest : BaseTest
{
    public DatabaseAccessTest() : base(null, null, null) { }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(1, 2, 1)]
    [Trait("Create Order Stored Procedure", "Validation")]
    public async Task CreateOrder_ReturnOrderIdAndTotal(int customerId, int productId, int quantity)
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        string sql = "EXEC [dbo].[CreateOrder] @CustomerId = @CustomerId, @ProductId = @ProductId, @Quantity = @Quantity, @Id = @Id OUT, @TotalCost = @TotalCost OUT";
        SqlParameter customerIdParameter = new SqlParameter("@CustomerId", customerId);
        SqlParameter productIdParameter = new SqlParameter("@ProductId", productId);
        SqlParameter quantityParameter = new SqlParameter("@Quantity", quantity);
        var idParameter = new SqlParameter { ParameterName = "@Id", Direction = ParameterDirection.Output, DbType = DbType.Int32 };
        var totalCostParameter = new SqlParameter { ParameterName = "@TotalCost", Direction = ParameterDirection.Output, DbType = DbType.Double };

        // Act
        await context.Database.ExecuteSqlRawAsync(sql, customerIdParameter, productIdParameter, quantityParameter, idParameter, totalCostParameter);

        var id = Convert.ToInt32(idParameter.Value);
        var totalCost = Convert.ToDouble(totalCostParameter.Value);

        // Assert
        Assert.True(id > 0);
        Assert.Equal(product.Price * quantity, totalCost);
    }
}