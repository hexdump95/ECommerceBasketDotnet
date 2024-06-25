using EcommerceBasket.Domain.Models;

namespace EcommerceBasket.Application.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> FindByUserId(string userId);
        Task<Basket> Save(Basket basket);
        Task<Basket> UpdateByUserId(string id, Basket basket);
        Task<bool> DeleteByUserId(string userId);
    }
}
