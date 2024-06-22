using EcommerceBasket.Application.Services.Interfaces;
using EcommerceBasket.Domain.Entities;
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

        public async Task<Basket> FindOne(Guid id)
        {
            return await _basketRepository.FindById(id)
                   ?? throw new NotImplementedException();
        }

        public async Task<Basket> Save(Basket basket)
        {
            basket.Id = Guid.NewGuid();
            return await _basketRepository.Save(basket);
        }

        public async Task<Basket> Update(Guid id, Basket basket)
        {
            var exists = await _basketRepository.ExistsById(id);
            if (!exists)
                throw new NotImplementedException();
            return await _basketRepository.Save(basket);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _basketRepository.Delete(id);
        }
    }
}
