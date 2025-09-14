using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public BookModel Book { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }
    }
}