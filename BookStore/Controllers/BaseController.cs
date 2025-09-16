using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class BaseController : Controller
{
    protected void SetCartCount()
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        var cart = string.IsNullOrEmpty(cartJson)
            ? new List<CartItem>()
            : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);

        ViewBag.CartCount = cart.Sum(c => c.Quantity);
    }
}
