using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleActive(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.LockoutEnabled = !user.LockoutEnabled;
        if (user.LockoutEnabled)
            user.LockoutEnd = DateTimeOffset.MaxValue;
        else
            user.LockoutEnd = null;

        await _userManager.UpdateAsync(user);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeRole(string id, string role)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, role);

        return RedirectToAction("Index");
    }
}
