namespace Moonpig.Basket.Api.Infrastructure;

public interface IBasketRepository
{
    Task<Models.Basket> GetBasket(string id);
    Task<Models.Basket> AddToBasket(string basketId, Models.LineItem lineItem);
}
