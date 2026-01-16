namespace Moonpig.Basket.Api.Services;

public interface IBasketService
{
    Task<Models.Basket> GetBasket(string id);
    Task<Models.Basket> AddToBasket(string basketId, Models.AddToBasketRequest request);
}
