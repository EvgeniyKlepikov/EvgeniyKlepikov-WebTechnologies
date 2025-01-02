using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.UI.Data;

namespace KLEPIKOV30323WEB.UI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly KLEPIKOV30323WEB.UI.Data.AppDbContext _context;

        public CreateModel(KLEPIKOV30323WEB.UI.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
