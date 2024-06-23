namespace EcommerceBasket.Infrastructure.Persistence
{
    public class BasketItemEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
