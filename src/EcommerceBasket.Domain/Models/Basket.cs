namespace EcommerceBasket.Domain.Models
{
    public class Basket
    {
        public string? UserId { get; set; } = null!;
        public List<BasketItem> Items { get; set; } = [];
        public DateTime UpdatedAt { get; set; }
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
