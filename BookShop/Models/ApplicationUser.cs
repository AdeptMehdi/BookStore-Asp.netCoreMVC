using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        // فیلدهای اضافی (اختیاری)
        public string FullName { get; set; }
        public string? Address { get; set; }
    }
}
