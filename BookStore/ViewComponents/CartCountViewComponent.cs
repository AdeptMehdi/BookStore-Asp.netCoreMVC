using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class CartCountViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);

            int count = cart.Sum(c => c.Quantity);
            return View(count);
        }
    }


}
