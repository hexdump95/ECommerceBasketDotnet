using System.Text.Json;

using EcommerceBasket.Application.Services.Interfaces;
using EcommerceBasket.Domain.Models;
using EcommerceBasket.Infrastructure;

using Microsoft.AspNetCore.Mvc;

using OpenTelemetry.Trace;

namespace EcommerceBasket.API.Controllers
{
    [ApiController]
    [Route("api/v1/basket")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly Tracer _tracer;

        public BasketController(ILogger<BasketController> logger, IBasketService basketService,
            JsonOptionsProvider jsonOptionsProvider, Tracer tracer)
        {
            _logger = logger;
            _basketService = basketService;
            _jsonOptions = jsonOptionsProvider.JsonOptions;
            _tracer = tracer;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetOne(string userId)
        {
            using var span = _tracer.StartActiveSpan("get-one");
            try
            {
                var basket = await _basketService.FindByUserId(userId);
                _logger.LogInformation("Basket found: {result}", JsonSerializer.Serialize(basket, _jsonOptions));
                return Ok(basket);
            }
            catch (Exception e)
            {
                span.SetStatus(Status.Error);
                span.RecordException(e);
                _logger.LogInformation("There was an error: {errorMessage}", e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Save([FromBody] Basket basket)
        {
            return Ok(await _basketService.Save(basket));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Basket>> Save(string userId, [FromBody] Basket basket)
        {
            return Ok(await _basketService.UpdateByUserId(userId, basket));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string userId)
        {
            return Ok(await _basketService.DeleteByUserId(userId));
        }
    }
}
