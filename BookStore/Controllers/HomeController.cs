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

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            Categories = _context.Categories.ToList(),
            NewBooks = _context.Books
                .OrderByDescending(b => b.CreatedDate)
                .Take(6)
                .ToList(),

            BestSellers = _context.Books
                .OrderByDescending(b => b.SalesCount)
                .Take(6)
                .ToList(),

            FeaturedBooks = _context.Books
                .Where(b => b.IsFeatured)
                .Take(6)
                .ToList()
        };

        return View(viewModel);
    }
}