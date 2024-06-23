namespace EcommerceBasket.Domain.Models
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
