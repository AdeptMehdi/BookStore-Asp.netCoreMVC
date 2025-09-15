// Models/OrderDetail.cs
namespace BookStore.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
