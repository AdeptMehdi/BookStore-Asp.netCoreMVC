using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }           
        public ApplicationUser User { get; set; }

        public Book Book { get; set; }
    }
}
