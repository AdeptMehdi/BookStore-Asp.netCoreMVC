using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookStore.Models;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);

        var cartItems = _context.CartItems
            .Include(c => c.Book)
            .Where(c => c.UserId == userId)
            .ToList();

        return View(cartItems);
    }

    [HttpGet]
    public IActionResult AddToCart(int id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var book = _context.Books.Find(id);
        if (book == null)
            return NotFound();

        var existingItem = _context.CartItems
            .FirstOrDefault(c => c.BookId == id && c.UserId == userId);

        if (existingItem != null)
        {
            existingItem.Quantity += 1;
        }
        else
        {
            var newItem = new CartItem
            {
                BookId = id,
                UserId = userId,
                Quantity = 1
            };
            _context.CartItems.Add(newItem);
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int id)
    {
        var item = _context.CartItems.Find(id);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Checkout()
    {
        var userId = _userManager.GetUserId(User);
        var cartItems = _context.CartItems
            .Include(i => i.Book)
            .Where(i => i.UserId == userId)
            .ToList();

        if (!cartItems.Any())
            return RedirectToAction("Index");

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Pending,
            TotalAmount = cartItems.Sum(i => (decimal)i.Book.Price * i.Quantity)
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        foreach (var item in cartItems)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                BookId = item.BookId,
                Quantity = item.Quantity,
                UnitPrice = (decimal)item.Book.Price
            };
            _context.OrderItems.Add(orderItem);
        }

        _context.CartItems.RemoveRange(cartItems);
        _context.SaveChanges();

        return RedirectToAction("Confirmation", new { id = order.Id });
    }

    public IActionResult Confirmation(int id)
    {
        var order = _context.Orders
            .Include(o => o.Items).ThenInclude(i => i.Book)
            .FirstOrDefault(o => o.Id == id);

        return View(order);
    }
    [Authorize]
    public IActionResult Invoice(int id)
    {
        var userId = _userManager.GetUserId(User);

        var order = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Book)
            .FirstOrDefault(o => o.Id == id && o.UserId == userId);

        if (order == null)
            return NotFound();

        return View(order);
    }
    [Authorize]
    public IActionResult OrderHistory()
    {
        var userId = _userManager.GetUserId(User);

        var orders = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Book)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return View(orders);
    }

}
