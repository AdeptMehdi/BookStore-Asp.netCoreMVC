using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Categories = await _context.Categories.ToListAsync(),

            NewBooks = await _context.Books
                .OrderByDescending(b => b.CreatedDate) // مرتب‌سازی بر اساس تاریخ ایجاد
                .Take(8)
                .ToListAsync(),

            BestSellers = await _context.Books
                .OrderByDescending(b => b.SalesCount) // مرتب‌سازی بر اساس تعداد فروش
                .Take(8)
                .ToListAsync(),

            FeaturedBooks = await _context.Books
                .Where(b => b.IsFeatured)
                .OrderByDescending(b => b.CreatedDate) // یا هر فیلدی که می‌خوای
                .Take(8)
                .ToListAsync()
        };

        return View(model);
    }

}