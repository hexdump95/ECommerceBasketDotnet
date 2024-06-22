namespace EcommerceBasket.Domain.Repositories
{
    public interface IRepository<TEntity, in TKey>
    {
        Task<List<TEntity>> FindAll();
        Task<TEntity?> FindById(TKey key);
        Task<TEntity> Save(TEntity entity);
        Task<bool> Delete(TKey key);
    }
}
