using BookStore.Extensions;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace BookStore.Controllers
{
    public class CheckoutController : Controller
    {
        private const string CartSessionKey = "Cart";

        // GET: /Checkout
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemModel>>(CartSessionKey) ?? new List<CartItemModel>();
            if (!cart.Any())
                return RedirectToAction("Index", "Home");

            ViewBag.Total = cart.Sum(c => c.Book.Price * c.Quantity);
            return View(cart);
        }

        // POST: /Checkout/PlaceOrder
        [HttpPost]
        public IActionResult PlaceOrder(string name, string email, string address)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemModel>>(CartSessionKey) ?? new List<CartItemModel>();
            if (!cart.Any()) return RedirectToAction("Index", "Home");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = cart.Select(c => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(c.Book.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = c.Book.Title,
                        },
                    },
                    Quantity = c.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Checkout", null, Request.Scheme),
                CancelUrl = Url.Action("Index", "Cart", null, Request.Scheme),
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return View();
        }
    }
}
