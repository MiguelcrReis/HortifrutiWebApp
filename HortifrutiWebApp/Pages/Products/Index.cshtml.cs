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
    public class IndexModel : PageModel
    {
        private readonly HortifrutiWebApp.Data.WebAppDbContext _context;

        public IndexModel(HortifrutiWebApp.Data.WebAppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
