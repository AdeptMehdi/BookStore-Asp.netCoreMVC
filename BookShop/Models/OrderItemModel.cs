using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public OrderModel Order { get; set; }

        public int BookId { get; set; }
        public BookModel Book { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
