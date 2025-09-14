using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // نقش‌ها
            string[] roles = { "Admin", "Customer", "Employee", "Company" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // کاربر ادمین پیش‌فرض
            var adminEmail = "admin@bookstore.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123"); // پسورد پیش‌فرض

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}