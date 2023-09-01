using OrderManagement.Application.Common.Enums;
using OrderManagement.Application.Features.Orders.Models;

namespace OrderManagement.Test.Tests;

public class OrderTest : BaseTest
{
    public OrderTest() : base("Orders", "manager@gmail.com", "12345678") {}

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(1, 2, 1)]
    [Trait("Create Order", "Validation")]
    public async Task ValidOrderAPI_ReturnTrue(int customerId, int productId, int quantity)
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var product = await unitOfWork.ProductRepository.GetById(productId);
        
        var command = new CreateOrderCommand
        {
            CustomerId = customerId,
            ProductId = product.Id,
            Quantity = quantity
        };

        // Act
        var response = await Post<CreateOrderCommand, Response<OrderDetails>>(command);

        // Assert
        Assert.True(response.Succeeded);
        Assert.True(response.Data.Id > 0);
        Assert.Equal(command.CustomerId, response.Data.CustomerId);
        Assert.Equal(command.ProductId, response.Data.ProductId);
        Assert.Equal(command.Quantity, response.Data.Quantity);
        Assert.Equal(product.Price*command.Quantity, response.Data.TotalCost);
    }

    [Theory]
    [InlineData(1,1,2)]
    [InlineData(1,2,1)]
    [Trait("Create Order", "Validation")]
    public async Task DataAccessForValidOrder_ReturnOrder(int customerId, int productId, int quantity)
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var product = await unitOfWork.ProductRepository.GetById(productId);

        var arrangeOrder = new Order
        {
            CustomerId = customerId,
            ProductId = product.Id,
            Quantity = quantity,
            TotalCost = product.Price * quantity
        };

        var createOrder = new Order
        {
            CustomerId = arrangeOrder.CustomerId,
            ProductId = arrangeOrder.ProductId,
            Quantity = arrangeOrder.Quantity
        };

        // Act
        await unitOfWork.OrderRepository.Add(createOrder);

        // Assert
        Assert.True(createOrder.Id > 0);
        Assert.Equal(createOrder.CustomerId, arrangeOrder.CustomerId);
        Assert.Equal(createOrder.ProductId, arrangeOrder.ProductId);
        Assert.Equal(createOrder.Quantity, arrangeOrder.Quantity);
        Assert.Equal(product.Price * createOrder.Quantity, arrangeOrder.TotalCost);
    }

    [Theory]
    [InlineData(1, 10, 2)]
    [InlineData(1, 20, 1)]
    [Trait("Create Order", "Validation")]
    public async Task InvalidProductOrder_ReturnProductNotFound(int customerId, int productId, int quantity)
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            CustomerId = customerId,
            ProductId = productId,
            Quantity = quantity
        };

        // Act
        var response = await Post<CreateOrderCommand, Response<OrderDetails>>(command);

        // Assert
        Assert.False(response.Succeeded);
        Assert.Contains(new Message { Type = MessageType.Error, Text = "Product not found" }, response.Messages);
    }

    [Theory]
    [InlineData(10, 1, 2)]
    [InlineData(10, 1, 1)]
    [Trait("Create Order", "Validation")]
    public async Task InvalidCustomerOrder_ReturnProductNotFound(int customerId, int productId, int quantity)
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            CustomerId = customerId,
            ProductId = productId,
            Quantity = quantity
        };

        // Act
        var response = await Post<CreateOrderCommand, Response<OrderDetails>>(command);

        // Assert
        Assert.False(response.Succeeded);
        Assert.Contains(new Message { Type = MessageType.Error, Text = "Customer not found" }, response.Messages);
    }
}
