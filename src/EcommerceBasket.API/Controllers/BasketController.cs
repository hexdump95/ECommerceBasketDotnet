using EcommerceBasket.Application.Services.Interfaces;
using EcommerceBasket.Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace EcommerceBasket.API.Controllers
{
    [ApiController]
    [Route("api/v1/basket")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;

        public BasketController(ILogger<BasketController> logger, IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetOne(Guid id)
        {
            return Ok(await _basketService.FindOne(id));
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
