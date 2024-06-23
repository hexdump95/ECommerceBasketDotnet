using EcommerceBasket.Domain.Models;

namespace EcommerceBasket.Domain.Repositories
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
        Task<bool> ExistsById(Guid id);
        Task<int> DeleteBasketRecordsToUpdatedAt(DateTime toDate);
    }
}
