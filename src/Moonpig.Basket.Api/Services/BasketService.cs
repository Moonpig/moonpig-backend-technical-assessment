using Moonpig.Basket.Api.Helpers;
using Moonpig.Basket.Api.Infrastructure;

namespace Moonpig.Basket.Api.Services;

public class BasketService(IBasketRepository basketRepository, ProductsHelper productsHelper) : IBasketService
{
    public async Task<Models.Basket> GetBasket(string id)
    {
        var basket = await basketRepository.GetBasket(id);
        
        if (basket?.LineItems != null)
        {
            foreach (var lineItem in basket.LineItems)
            {
                var product = await productsHelper.GetProduct(lineItem.ProductId);
                lineItem.ProductName = product.ProductName;
            }
        }
        
        return basket;
    }

    public async Task<Models.Basket> AddToBasket(string basketId, Models.AddToBasketRequest request)
    {
        var product = await productsHelper.GetProduct(request.ProductId);

        var lineItem = new Models.LineItem
        {
            ProductId = request.ProductId,
            ProductName = product.ProductName,
            ImagePreviewUrl = product.ImagePreviewUrl,
            Price = new Models.Price { Amount = product.Price.Amount, Currency = "GBP" },
            Quantity = request.Quantity
        };

        return await basketRepository.AddToBasket(basketId, lineItem);
    }
}
