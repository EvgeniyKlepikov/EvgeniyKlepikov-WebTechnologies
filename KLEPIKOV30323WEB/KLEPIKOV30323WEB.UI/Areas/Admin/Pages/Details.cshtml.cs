using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.UI.Data;

namespace KLEPIKOV30323WEB.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly KLEPIKOV30323WEB.UI.Data.AppDbContext _context;

        public DetailsModel(KLEPIKOV30323WEB.UI.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
