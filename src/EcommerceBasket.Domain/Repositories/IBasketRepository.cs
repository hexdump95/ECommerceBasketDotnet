using EcommerceBasket.Domain.Entities;

namespace EcommerceBasket.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<List<Basket>> FindAll();
        Task<Basket?> FindOne(Guid id);
        Task<Basket> Save(Basket basket);
        Task<bool> Delete(Guid id);
        Task<bool> ExistsById(Guid id);
    }
}
