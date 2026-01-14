namespace Moonpig.Basket.Api.Tests;

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Controllers;
using Services;
using Moq;
using Xunit;

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
        var expectedBasket = new Models.Basket { Id = expectedBasketId };

        _mockBasketService
            .Setup(x => x.GetBasket(expectedBasketId))
            .ReturnsAsync(expectedBasket);

        var result = await _sut.Get(1);

        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeOfType<Models.Basket>();
        (okResult.Value as Models.Basket)!.Id.Should().Be(expectedBasketId);
    }

    [Fact]
    public async Task WhenBasketDoesNotExist_ShouldReturnNotFound()
    {
        const int basketId = 99;
        _mockBasketService
            .Setup(x => x.GetBasket(basketId.ToString()))!
            .ReturnsAsync(null as Models.Basket);
        
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
}