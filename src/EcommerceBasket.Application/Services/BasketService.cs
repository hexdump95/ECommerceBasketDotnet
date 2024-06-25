using EcommerceBasket.Application.Services.Interfaces;
using EcommerceBasket.Domain.Models;
using EcommerceBasket.Domain.Repositories;

namespace EcommerceBasket.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> FindByUserId(string userId)
        {
            return await _basketRepository.FindByUserId(userId)
                   ?? throw new NotImplementedException();
        }

        public async Task<Basket> Save(Basket basket)
        {
            basket.UpdatedAt = DateTime.UtcNow;
            return await _basketRepository.Save(basket);
        }

        public async Task<Basket> UpdateByUserId(string userId, Basket basket)
        {
            var exists = await _basketRepository.ExistsByUserId(userId);
            if (!exists)
                throw new NotImplementedException();
            basket.UpdatedAt = DateTime.UtcNow;
            return await _basketRepository.Save(basket);
        }

        public async Task<bool> DeleteByUserId(string userId)
        {
            return await _basketRepository.Delete(userId);
        }
    }
}
