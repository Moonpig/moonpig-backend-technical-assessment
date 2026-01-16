using Flurl;
using Flurl.Http;
using Moonpig.Basket.Api.Models;

namespace Moonpig.Basket.Api.Helpers;

public class ProductsHelper
{
    public Task<Product> GetProduct(string productId)
    {
        // Console.WriteLine($"Getting product {productId}");
        // Console.WriteLine($"Time: {DateTime.Now}");
        // System.Diagnostics.Debug.WriteLine($"API call for product: {productId}");
        
        var product = "http://localhost:3000"
            .AppendPathSegments("products", productId)
            .GetJsonAsync<Product>()
            .Result;

        // Console.WriteLine($"Product retrieved: {product.ProductName}");
        // Console.WriteLine($"Price: {product.Price?.Amount}");

        return Task.FromResult(new Product
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            ImagePreviewUrl = product.ImagePreviewUrl,
            Price = product.Price,
            
        });
    }
    
    public string GetCachedProductData(string productId)
    {
        var cachePath = Path.Combine(Directory.GetCurrentDirectory(), $"cache-{productId}.json");
        
        if (File.Exists(cachePath))
        {
            var reader = new StreamReader(cachePath);
            var content = reader.ReadToEnd();
            return content;
        }
        
        return null;
    }
}