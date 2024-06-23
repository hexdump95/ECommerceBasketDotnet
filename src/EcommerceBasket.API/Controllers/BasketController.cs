using System.Text.Json;

using EcommerceBasket.Application.Services.Interfaces;
using EcommerceBasket.Domain.Models;
using EcommerceBasket.Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace EcommerceBasket.API.Controllers
{
    [ApiController]
    [Route("api/v1/basket")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;
        private readonly JsonSerializerOptions _jsonOptions;

        public BasketController(ILogger<BasketController> logger, IBasketService basketService,
            JsonOptionsProvider jsonOptionsProvider)
        {
            _logger = logger;
            _basketService = basketService;
            _jsonOptions = jsonOptionsProvider.JsonOptions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetOne(Guid id)
        {
            try
            {
                var basket = await _basketService.FindOne(id);
                _logger.LogInformation("Basket found: {result}", JsonSerializer.Serialize(basket, _jsonOptions));
                return Ok(basket);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("There was an error: {errorMessage}", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Save([FromBody] Basket basket)
        {
            return Ok(await _basketService.Save(basket));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Basket>> Save(Guid id, [FromBody] Basket basket)
        {
            return Ok(await _basketService.Update(id, basket));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await _basketService.Delete(id));
        }
    }
}
