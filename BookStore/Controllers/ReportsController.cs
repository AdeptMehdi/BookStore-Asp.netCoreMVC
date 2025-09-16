using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize(Roles = "Admin")]
public class ReportsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var todaySales = _context.Orders
            .Where(o => o.Status == OrderStatus.Paid && o.OrderDate.Date == DateTime.Today)
            .Sum(o => o.TotalAmount);

        var monthlySales = _context.Orders
            .Where(o => o.Status == OrderStatus.Paid && o.OrderDate.Month == DateTime.Now.Month)
            .Sum(o => o.TotalAmount);

        var topBooks = _context.OrderItems
            .GroupBy(i => i.Book.Title)
            .Select(g => new { Title = g.Key, Count = g.Sum(i => i.Quantity) })
            .OrderByDescending(g => g.Count)
            .Take(5)
            .ToList();

        ViewBag.TodaySales = todaySales;
        ViewBag.MonthlySales = monthlySales;
        ViewBag.TopBooks = topBooks;

        return View();
    }
}
