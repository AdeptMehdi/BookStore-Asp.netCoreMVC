using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();

        var userRoles = new Dictionary<string, string>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRoles[user.Id] = roles.FirstOrDefault() ?? "User";
        }

        ViewBag.UserRoles = userRoles;
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleActive(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            TempData["error"] = "کاربر پیدا نشد.";
            return RedirectToAction("Index");
        }

        if (user.LockoutEnd == null || user.LockoutEnd <= DateTimeOffset.UtcNow)
        {
            user.LockoutEnd = DateTimeOffset.MaxValue;
            TempData["success"] = $"کاربر {user.FullName} غیرفعال شد.";
        }
        else
        {
            user.LockoutEnd = null;
            TempData["success"] = $"کاربر {user.FullName} فعال شد.";
        }

        await _userManager.UpdateAsync(user);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeRole(string id, string role)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            TempData["error"] = "کاربر پیدا نشد.";
            return RedirectToAction("Index");
        }

        if (!await _roleManager.RoleExistsAsync(role))
        {
            TempData["error"] = $"نقش {role} وجود ندارد.";
            return RedirectToAction("Index");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, role);

        TempData["success"] = $"نقش کاربر {user.FullName} به {role} تغییر کرد.";
        return RedirectToAction("Index");
    }
}
