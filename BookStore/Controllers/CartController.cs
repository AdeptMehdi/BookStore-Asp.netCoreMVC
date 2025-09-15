using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    public IActionResult AddToCart(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return NotFound();

        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.BookId == id);
        if (item == null)
        {
            cart.Add(new CartItem { BookId = id, Title = book.Title, Price = book.Price, Quantity = 1 });
        }
        else
        {
            item.Quantity++;
        }
        SaveCart(cart);
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int id)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.BookId == id);
        if (item != null) cart.Remove(item);
        SaveCart(cart);
        return RedirectToAction("Index");
    }

    private List<CartItem> GetCart()
    {
        var cart = HttpContext.Session.GetString("Cart");
        return cart == null ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }
}
