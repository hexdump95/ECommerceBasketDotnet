namespace EcommerceBasket.Domain.Entities
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
