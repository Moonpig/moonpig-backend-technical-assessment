namespace Moonpig.Basket.Api.Tests;

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Controllers;
using Services;
using Moq;
using Xunit;
using Models;

public class BasketControllerTests
{
    private readonly BasketController _sut;
    private readonly Mock<IBasketService> _mockBasketService;

    public BasketControllerTests()
    {
        _mockBasketService = new Mock<IBasketService>();
        
        _sut = new BasketController(_mockBasketService.Object);
    }
    
    [Fact]
    public async Task ShouldReturnOk()
    {
        const string expectedBasketId = "1";
        var expectedBasket = new Basket { Id = expectedBasketId };

        _mockBasketService
            .Setup(x => x.GetBasket(expectedBasketId))
            .ReturnsAsync(expectedBasket);

        var result = await _sut.Get(1);

        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeOfType<Basket>();
        (okResult.Value as Basket)!.Id.Should().Be(expectedBasketId);
    }

    [Fact]
    public async Task ShouldReturnNotFound()
    {
        const int basketId = 99;
        _mockBasketService
            .Setup(x => x.GetBasket(basketId.ToString()))!
            .ReturnsAsync(null as Basket);
        
        var result = await _sut.Get(basketId);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task WhenBasketIdIsNonPositive_ShouldReturnBadRequest()
    {
        const int basketId = 0;
        
        var result = await _sut.Get(basketId);

        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult!.Value.Should().Be("Basket ID must be a positive number.");
    }

    [Fact]
    public async Task AddToBasket_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        const int basketId = 1;
        var request = new AddToBasketRequest { ProductId = "PROD123", Quantity = 2 };
        var expectedBasket = new Basket { Id = basketId.ToString() };

        _mockBasketService
            .Setup(x => x.AddToBasket(basketId.ToString(), request))
            .ReturnsAsync(expectedBasket);

        // Act
        var result = await _sut.AddToBasket(basketId, request);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeOfType<Basket>();
        (okResult.Value as Basket)!.Id.Should().Be(basketId.ToString());
    }

    [Fact]
    public async Task AddToBasket_WhenBasketIdIsNonPositive_ShouldReturnBadRequest()
    {
        // Arrange
        const int basketId = 0;
        var request = new AddToBasketRequest { ProductId = "PROD123", Quantity = 2 };

        // Act
        var result = await _sut.AddToBasket(basketId, request);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult!.Value.Should().Be("Basket ID must be a positive number.");
    }

    [Fact]
    public async Task AddToBasket_ShouldReturnBadRequest_WhenQuantityIsNonPositive()
    {
        var basketRepository = new Infrastructure.InMemoryBasketRepository();
        var productsHelper = new Helpers.ProductsHelper();
        var basketService = new BasketService(basketRepository, productsHelper);
        var sut = new BasketController(basketService);
        
        var request = new AddToBasketRequest { ProductId = "CARD123", Quantity = 2 };
        
        var result = await sut.AddToBasket(1, request);

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task AddToBasket_WhenProductIdIsEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        const int basketId = 1;
        var request = new AddToBasketRequest { ProductId = string.Empty, Quantity = 2 };

        // Act
        var result = await _sut.AddToBasket(basketId, request);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        result.Result.Should().NotBeNull();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult!.Value.Should().Be("Product ID is required.");
        badRequestResult.Value.Should().NotBeNull();
        badRequestResult.Value.Should().BeOfType<string>();
        ((string)badRequestResult.Value).Should().NotBeEmpty();
        ((string)badRequestResult.Value).Length.Should().BeGreaterThan(10);
    }
}
