using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.UI.Data;
using Microsoft.AspNetCore.Mvc;

namespace KLEPIKOV30323WEB.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<Cart>("cart");
            return View(cart);
        }
    }
    //public class CartViewModel
    //{
    //    public string Total { get; set; }
    //    public int ItemCount { get; set; }
    //}
}
