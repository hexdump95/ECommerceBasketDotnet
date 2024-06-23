namespace EcommerceBasket.Infrastructure.Persistence
{
    public class BasketEntity
    {
        public string? UserId { get; set; }
        public List<BasketItemEntity> Items { get; set; } = [];
    }
}
