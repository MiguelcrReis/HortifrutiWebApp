using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebAppDbContext _context;
        public IList<Product> Products { get; set; }

        public IndexModel(WebAppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null) { return NotFound(); }

            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
