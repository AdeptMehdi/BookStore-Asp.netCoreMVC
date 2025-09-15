namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public double TotalAmount { get; set; }
        public string PaymentStatus { get; set; } // Pending, Paid, Failed

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
