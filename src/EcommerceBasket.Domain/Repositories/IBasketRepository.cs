using EcommerceBasket.Domain.Models;

namespace EcommerceBasket.Domain.Repositories
{
    public interface IBasketRepository : IRepository<Basket, string>
    {
        Task<Basket?> FindByUserId(string userId);
        Task<bool> DeleteByUserId(string userId);
        Task<bool> ExistsByUserId(string userId);
        Task<int> DeleteBasketRecordsToUpdatedAt(DateTime toDate);
    }
}
