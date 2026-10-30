namespace Moonpig.Basket.Api.Models;

public class AddToBasketRequest
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
} 