using Moonpig.Basket.Api.Infrastructure;

namespace Moonpig.Basket.Api.Services;

public class BasketService(IBasketRepository basketRepository) : IBasketService
{
    public async Task<Models.Basket> GetBasket(string id)
    {
        var basket = await basketRepository.GetBasket(id);
        return basket;
    }
}
