using Inno_Shop.Product.API.Controllers;
using Inno_Shop.Product.Application.CQRS.Command;
using Inno_Shop.Product.Application.CQRS.Query;
using Inno_Shop.Product.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Inno_Shop_Auth.Tests;

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly ProductController _productController;
    
    public ProductControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _productController = new ProductController(_mockMediator.Object);
    }

    [Fact]
    public async Task CreateProduct_ReturnUnit_WhenCommandIsSent()
    {
        //Arrange
        var productDto = new ProductDTO()
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(),default))
            .ReturnsAsync(Unit.Value);
        
        //Act
        var result = await _productController.CreateProduct(productDto);
        
        //Assert
        Assert.Equal(Unit.Value, result);
        _mockMediator.Verify(m => m.Send(It.IsAny<CreateProductCommand>(), default), Times.Once);
    }
    
    [Fact]
    public async Task GetProduct_ReturnsProductDTO_WhenProductExists()
    {
        var productId = 1;
        // Arrange
        var productDto = new ProductDTO
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            Price = 100
        };
    
        _mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), default))
            .ReturnsAsync(productDto);
    
        // Act
        var result = await _productController.GetProduct(productDto.Id);
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ProductDTO>(okResult.Value);
        Assert.Equal(productId, returnValue.Id);
        _mockMediator.Verify(m => m.Send(It.IsAny<GetProductQuery>(), default), Times.Once);
    }
    
    [Fact]
    public async Task GetProducts_ReturnsListOfProductDTOs_WhenCalled()
    {
        // Arrange
        var products = new List<ProductDTO>
        {
            new ProductDTO { Id = 1, Name = "Test Product 1", Description = "Description 1", Price = 100 },
            new ProductDTO { Id = 2, Name = "Test Product 2", Description = "Description 2", Price = 200 }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
            .ReturnsAsync(products);

        // Act
        var result = await _productController.GetProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<ProductDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
        _mockMediator.Verify(m => m.Send(It.IsAny<GetProductsQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsUnit_WhenCommandIsSent()
    {
        // Arrange
        var productId = 1;

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _productController.DeleteProduct(productId);

        // Assert
        Assert.Equal(Unit.Value, result);
        _mockMediator.Verify(m => m.Send(It.IsAny<DeleteProductCommand>(), default), Times.Once);
    }
}