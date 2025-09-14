using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        // Navigation Property
        public ICollection<BookModel> Books { get; set; }
    }
}