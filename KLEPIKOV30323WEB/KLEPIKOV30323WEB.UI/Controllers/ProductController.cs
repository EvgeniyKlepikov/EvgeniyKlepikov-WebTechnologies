using Microsoft.AspNetCore.Mvc;

namespace KLEPIKOV30323WEB.UI.Controllers
{
    public class ProductController(ICategoryService categoryService,
    IProductService productService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var productResponse =
            await
            productService.GetProductListAsync(null);
            if (!productResponse.Success)
                return NotFound(productResponse.ErrorMessage);
            return View(productResponse.Data.Items);
        }
    }
}
