namespace Moonpig.Basket.Api.Models;

public class Basket
{
    public string Id { get; set; }
    public List<LineItem> LineItems { get; set; } = new();
}
