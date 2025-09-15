using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }
    [Authorize(Roles = "Admin")]
    // لیست همه نظرات
    public async Task<IActionResult> Index()
    {
        var reviews = await _context.Reviews
            .Include(r => r.Book)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return View(reviews);
    }

    // حذف نظر
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return NotFound();

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "Admin")]
    // ویرایش نظر
    public async Task<IActionResult> Edit(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return NotFound();

        return View(review);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Review review)
    {
        if (!ModelState.IsValid)
            return View(review);

        _context.Update(review);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}