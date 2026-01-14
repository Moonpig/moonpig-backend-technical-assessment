namespace Moonpig.Basket.Api.Services;

public interface IBasketService
{
    Task<Models.Basket> GetBasket(string id);
}
