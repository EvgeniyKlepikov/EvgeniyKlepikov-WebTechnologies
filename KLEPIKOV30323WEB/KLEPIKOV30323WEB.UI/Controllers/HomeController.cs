using Microsoft.AspNetCore.Mvc;

namespace KLEPIKOV30323WEB.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
