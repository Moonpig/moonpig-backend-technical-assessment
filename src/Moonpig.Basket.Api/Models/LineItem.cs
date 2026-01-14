namespace Moonpig.Basket.Api.Models;

public class LineItem
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImagePreviewUrl { get; set; }
    public Price Price { get; set; }
    public int Quantity { get; set; }
}
