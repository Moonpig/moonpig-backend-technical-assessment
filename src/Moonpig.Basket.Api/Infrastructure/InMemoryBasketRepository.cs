using Moonpig.Basket.Api.Models;

namespace Moonpig.Basket.Api.Infrastructure;
public class InMemoryBasketRepository : IBasketRepository
{
    private readonly Dictionary<string, Models.Basket> _store = new Dictionary<string, Models.Basket>
    {
        { "1", new Models.Basket
            {
                Id = "1",
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        ProductId = "CHOC123",
                        ProductName = "Tony's Rainbow Chocolate Tasting Pack (288g)",
                        ImagePreviewUrl = "https://placehold.co/160x160",
                        Price = new Price { Amount = 12.49m, Currency = "GBP" },
                        Quantity = 1
                    },
                    new LineItem
                    {
                        ProductId = "CARD123",
                        ProductName = "Kate Smith Company Birthday Card",
                        ImagePreviewUrl = "https://placehold.co/160x160",
                        Price = new Price { Amount = 3.99m, Currency = "USD" },
                        Quantity = 2
                    }
                }
            } 
        }
    };

    public Task<Models.Basket> GetBasket(string id)
    {
        _store.TryGetValue(id, out var basket);
        return Task.FromResult(basket);
    }

    public Task<Models.Basket> AddToBasket(string bid, Models.LineItem line_item)
    {
        if (!_store.TryGetValue(bid, out var basket))
        {
            basket = new Models.Basket { Id = bid, LineItems = new List<LineItem>() };
            _store[bid] = basket;
        }

        var existingItem = basket.LineItems.FirstOrDefault(x => x.ProductId == line_item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += line_item.Quantity;
        }
        else
        {
            basket.LineItems.Add(line_item);
        }

        return Task.FromResult(basket);
    }
}
