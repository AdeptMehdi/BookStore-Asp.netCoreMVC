// Models/Book.cs
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string ISBN { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است")]
        public int CategoryId { get; set; }

        [ValidateNever]            
        public Category Category { get; set; }  // nullable
        // پراپرتی ناوبری برای نظرات
        [ValidateNever]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }

}
