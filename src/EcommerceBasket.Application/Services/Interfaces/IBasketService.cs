using EcommerceBasket.Domain.Models;

namespace EcommerceBasket.Application.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> FindOne(Guid id);
        Task<Basket> Save(Basket basket);
        Task<Basket> Update(Guid id, Basket basket);
        Task<bool> Delete(Guid id);
    }
}
