using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var orders = _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items).ThenInclude(i => i.Book)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
        return View(orders);
    }

    [HttpPost]
    public IActionResult UpdateStatus(int id, OrderStatus status)
    {
        var order = _context.Orders.Find(id);
        if (order == null) return NotFound();

        order.Status = status;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
