using Microsoft.AspNetCore.Mvc;
using Moonpig.Basket.Api.Services;

namespace Moonpig.Basket.Api.Controllers;

[Route("api/[controller]")]
public class BasketController(IBasketService basketService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Basket>> Get([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest("Basket ID must be a positive number.");
        }
        var basket = await basketService.GetBasket(id.ToString());
        if (basket == null)
        {
            return NotFound();
        }
        return Ok(basket);
    }

    [HttpPost("{id}/add_to_basket")]
    public async Task<ActionResult<Models.Basket>> AddToBasket([FromRoute] int id, [FromBody] Models.AddToBasketRequest request)
    {
        
        if (id <= 0)
        {
            return BadRequest("Basket ID must be a positive number.");
        }

        if (request.Quantity <= 1)
        {
            return BadRequest("Quantity must be valid.");
        }

        if (string.IsNullOrWhiteSpace(request.ProductId))
        {
            return BadRequest("Product ID is required.");
        }

        var basket = await basketService.AddToBasket(id.ToString(), request);
        return Ok(basket);
    }
}
