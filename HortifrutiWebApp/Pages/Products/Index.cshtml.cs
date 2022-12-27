using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebAppDbContext _context;

        public IndexModel(WebAppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
