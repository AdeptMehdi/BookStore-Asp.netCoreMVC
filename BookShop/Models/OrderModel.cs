using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public string UserId { get; set; } // ارتباط با IdentityUser

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public string ShippingAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Shipped, Completed

        // Navigation
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}