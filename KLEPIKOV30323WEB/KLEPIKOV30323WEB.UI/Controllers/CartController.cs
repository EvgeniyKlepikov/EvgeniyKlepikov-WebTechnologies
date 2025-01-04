using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.UI.Data;
using KLEPIKOV30323WEB.UI.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KLEPIKOV30323WEB.UI.Controllers
{
    //public static class SessionExtension
    //{
    //    public static void Set<T>(this ISession session, string key, T item)
    //    {
    //        var serializedItem = JsonSerializer.Serialize(item);
    //        session.SetString(key, serializedItem);
    //    }
    //    public static T Get<T>(this ISession session, string key)
    //    {
    //        var item = session.GetString(key);
    //        return item == null
    //        ? Activator.CreateInstance<T>() // или default(T)
    //        : JsonSerializer.Deserialize<T>(item);
    //    }
    //}

    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private Cart _cart;
        public CartController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: CartController
        public ActionResult Index()
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            return View(_cart.CartItems);
        }
        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _productService.GetProductByIdAsync(id);
            if (data.Success)
            {
                _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
                _cart.AddToCart(data.Data);
                HttpContext.Session.Set<Cart>("cart", _cart);
            }
            return Redirect(returnUrl);
        }
        [Route("[controller]/remove/{id:int}")]
        public ActionResult Remove(int id)
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            _cart.RemoveItems(id);
            HttpContext.Session.Set<Cart>("cart", _cart);
            return RedirectToAction("index");
        }
    }
}
