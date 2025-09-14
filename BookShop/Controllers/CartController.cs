using BookStore.Extensions;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";

        // GET: /Cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemModel>>(CartSessionKey) ?? new List<CartItemModel>();
            ViewBag.Total = cart.Sum(item => item.Book.Price * item.Quantity);
            return View(cart);
        }

        // POST: /Cart/Update
        [HttpPost]
        public IActionResult Update(List<CartItemModel> items)
        {
            HttpContext.Session.SetObject(CartSessionKey, items);
            return RedirectToAction("Index");
        }

        // GET: /Cart/Remove/5
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemModel>>(CartSessionKey) ?? new List<CartItemModel>();
            var item = cart.FirstOrDefault(c => c.Book.Id == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObject(CartSessionKey, cart);
            }
            return RedirectToAction("Index");
        }
    }
}