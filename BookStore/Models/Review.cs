using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [ValidateNever]
        public Book Book { get; set; }

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "متن نظر را وارد کنید")]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}