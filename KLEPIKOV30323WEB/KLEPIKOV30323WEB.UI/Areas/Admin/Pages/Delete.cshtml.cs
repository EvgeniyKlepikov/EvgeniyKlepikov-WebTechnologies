using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.UI.Data;
using KLEPIKOV30323WEB.UI.Services.ProductService;

namespace KLEPIKOV30323WEB.UI.Areas.Admin.Pages
{
    //public class DeleteModel : PageModel
    //{
    //    private readonly IProductService _productService;
    //    private readonly IWebHostEnvironment _environment;
    //    private readonly ILogger<DeleteModel> _logger;

    //    public DeleteModel(IProductService productService, IWebHostEnvironment environment, ILogger<DeleteModel> logger)
    //    {
    //        _productService = productService;
    //        _environment = environment;
    //        _logger = logger;
    //    }

    //    [BindProperty]
    //    public Product Product { get; set; }

    //    public async Task<IActionResult> OnGetAsync(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var response = await _productService.GetProductByIdAsync(id.Value);
    //        if (!response.Success || response.Data == null)
    //        {
    //            return NotFound();
    //        }

    //        Product = response.Data;
    //        return Page();
    //    }

    //    public async Task<IActionResult> OnPostAsync(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var response = await _productService.GetProductByIdAsync(id.Value);
    //        if (!response.Success || response.Data == null)
    //        {
    //            return NotFound();
    //        }

    //        // Удаление изображения, если оно существует
    //        if (!string.IsNullOrEmpty(response.Data.Image))
    //        {
    //            var fileName = Path.GetFileName(new Uri(response.Data.Image).AbsolutePath);
    //            var filePath = Path.Combine(_environment.WebRootPath, "Images", fileName);
    //            if (System.IO.File.Exists(filePath))
    //            {
    //                try
    //                {
    //                    System.IO.File.Delete(filePath);
    //                }
    //                catch (System.Exception ex)
    //                {
    //                    _logger.LogError(ex, "Error deleting image file: {FileName}", filePath);
    //                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the image file.");
    //                    return Page();
    //                }
    //            }
    //        }

    //        var deleteResult = await _productService.DeleteProductAsync(id.Value);
    //        if (!deleteResult.Success)
    //        {
    //            ModelState.AddModelError(string.Empty, "An error occurred while deleting the certification: " + deleteResult.ErrorMessage);
    //            return Page();
    //        }

    //        return RedirectToPage("./Index");
    //    }
    //}

    public class DeleteModel : PageModel
    {
        private readonly KLEPIKOV30323WEB.UI.Data.AppDbContext _context;

        public DeleteModel(KLEPIKOV30323WEB.UI.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                Product = product;
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
