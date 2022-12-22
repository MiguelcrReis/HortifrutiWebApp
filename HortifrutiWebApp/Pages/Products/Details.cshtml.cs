using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models;

namespace HortifrutiWebApp.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly HortifrutiWebApp.Data.WebAppDbContext _context;

        public DetailsModel(HortifrutiWebApp.Data.WebAppDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.IdProduct == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
