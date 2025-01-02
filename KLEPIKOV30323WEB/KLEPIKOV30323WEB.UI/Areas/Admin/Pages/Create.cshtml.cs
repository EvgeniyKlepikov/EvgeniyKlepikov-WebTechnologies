using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.UI.Data;
using KLEPIKOV30323WEB.UI.Services.CategoryService;
using KLEPIKOV30323WEB.UI.Services.ProductService;

namespace KLEPIKOV30323WEB.UI.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, IProductService productService) : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var categoryListData = await categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await productService.CreateProductAsync(Product, Image);

            return RedirectToPage("./Index");
        }
    }
}
