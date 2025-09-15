using BookStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalBooks = _context.Books.Count();
            ViewBag.TotalCategories = _context.Categories.Count();
            ViewBag.TotalReviews = _context.Reviews.Count();
            //ViewBag.PendingReviews = _context.Reviews
            ViewBag.PendingReviews = 0;

            return View();
        }
    }
}