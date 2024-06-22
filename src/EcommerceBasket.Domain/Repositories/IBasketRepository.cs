using EcommerceBasket.Domain.Entities;

namespace EcommerceBasket.Domain.Repositories
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
        Task<bool> ExistsById(Guid id);
    }
}
