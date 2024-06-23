namespace EcommerceBasket.Domain.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public List<BasketItem> Items { get; set; } = [];
        public DateTime UpdatedAt { get; set; }
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
