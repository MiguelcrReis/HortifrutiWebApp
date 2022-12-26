using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly WebAppDbContext _context;

        public IList<Product> Products;

        public IndexModel(ILogger<IndexModel> logger, WebAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Products = await _context.Product.ToListAsync<Product>();
        }
    }
}
