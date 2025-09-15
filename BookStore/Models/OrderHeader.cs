// Models/OrderHeader.cs
using System;

namespace BookStore.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = "";
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";

        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; } = "Pending";
        public string PaymentStatus { get; set; } = "Pending";
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
    }

}

