using OrderManagement.Application.Common.Enums;
using OrderManagement.Application.Features.Products.Model;

namespace OrderManagement.Test.Tests;

public class ProductTest : BaseTest
{
    public ProductTest() : base("Products", "manager@gmail.com", "12345678") { }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [Trait("Get Product By Id", "Validation")]
    public async Task ValidProduct_ReturnProduct(int productId)
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var product = await unitOfWork.ProductRepository.GetById(productId);

        // Act
        var response = await Get<Response<ProductDetails>>(productId.ToString());

        // Assert
        Assert.True(response.Succeeded);
        Assert.Equal(product.Id, response.Data.Id);
        Assert.Equal(product.Name, response.Data.Name);
        Assert.Equal(product.Price, response.Data.Price);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    [Trait("Get Product By Id", "Validation")]
    public async Task ProductNotFound_ReturnProductNotFound(int productId)
    {
        // Act
        var response = await Get<Response<ProductDetails>>(productId.ToString());

        // Assert
        Assert.False(response.Succeeded);
        Assert.Contains(new Message { Type = MessageType.Error, Text = "Product not found" }, response.Messages);
    }
}

