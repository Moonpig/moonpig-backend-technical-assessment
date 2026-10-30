using System.Text.Json.Serialization;

namespace Moonpig.Basket.Api.Models;

public class Product
{
    [JsonPropertyName("id")]
    public string ProductId { get; set; }
    
    [JsonPropertyName("name")]
    public string ProductName { get; set; }
    
    [JsonPropertyName("previewUrl")]
    public string ImagePreviewUrl { get; set; }
    
    public Price Price { get; set; }
}