using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } = "Pending";

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}