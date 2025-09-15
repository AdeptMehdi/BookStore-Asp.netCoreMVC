// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace BookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
         public string FullName { get; set; } // اضافه شد
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
    }

}
