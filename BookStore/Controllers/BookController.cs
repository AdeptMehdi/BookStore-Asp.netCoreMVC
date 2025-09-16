using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var books = _context.Books.Include(b => b.Category);
            return View(await books.ToListAsync());
        }
        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int bookId, string userName, string comment)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(comment))
            {
                TempData["error"] = "لطفاً همه فیلدها را پر کنید";
                return RedirectToAction("Details", new { id = bookId });
            }

            var review = new Review
            {
                BookId = bookId,
                UserName = userName,
                Comment = comment
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["success"] = "نظر شما با موفقیت ثبت شد";
            return RedirectToAction("Details", new { id = bookId });
        }





        // GET: Book/Create
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name");
            return View(new Book()); 
        }


        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile? file)
        {
            // نادیده گرفتن اعتبارسنجی پراپرتی ناوبری
            ModelState.Remove(nameof(Book.Category));

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
                return View(book);
            }
            else
            {
                TempData["debug"] += " | ModelState معتبر است";
            }

            // ذخیره تصویر
            if (file != null)
            {
                TempData["debug"] += " | فایل آپلود شده: " + file.FileName;

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(wwwRootPath, "images", "books", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                book.ImageUrl = "/images/books/" + fileName;
            }
            else
            {
                TempData["debug"] += " | فایل آپلود نشده، استفاده از عکس پیش‌فرض";
                book.ImageUrl = "/images/books/no-image.png";
            }

            // ذخیره در دیتابیس با لاگ قبل و بعد
            try
            {
                TempData["debug"] += " | قبل از ذخیره";
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                TempData["debug"] += $" | بعد از ذخیره (Id={book.Id})";
                TempData["success"] = "کتاب با موفقیت اضافه شد";
            }
            catch (Exception ex)
            {
                TempData["debug"] += " | خطا در ذخیره: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }



        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }


        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile? file)
        {
            if (id != book.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
                return View(book);
            }

            try
            {
                if (file != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(wwwRootPath, "images", "books", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    book.ImageUrl = "/images/books/" + fileName;
                }

                _context.Update(book);
                await _context.SaveChangesAsync();
                TempData["success"] = "کتاب با موفقیت ویرایش شد";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == book.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            TempData["success"] = "کتاب با موفقیت حذف شد";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Search(string query)
        {
            var results = _context.Books
                .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
                .ToList();

            ViewBag.Query = query;
            return View(results);
        }



    }
}
