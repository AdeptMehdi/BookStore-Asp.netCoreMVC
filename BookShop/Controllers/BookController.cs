using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;

namespace BookShop.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Chapters)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,Price,Quantity,ISBN,CoverImage,CategoryId")] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", bookModel.CategoryId);
            return View(bookModel);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", bookModel.CategoryId);
            return View(bookModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Author,Price,Quantity,ISBN,CoverImage,CategoryId")] BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", bookModel.CategoryId);
            return View(bookModel);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookModel = await _context.Books.FindAsync(id);
            if (bookModel != null)
            {
                _context.Books.Remove(bookModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
