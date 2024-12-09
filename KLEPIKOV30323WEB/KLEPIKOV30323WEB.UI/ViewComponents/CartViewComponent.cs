using Microsoft.AspNetCore.Mvc;

namespace KLEPIKOV30323WEB.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
