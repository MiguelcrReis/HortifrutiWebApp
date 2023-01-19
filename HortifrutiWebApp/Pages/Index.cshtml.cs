using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
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
        private const int PageSize = 12;
        private readonly ILogger<IndexModel> _logger;

        private readonly WebAppDbContext _context;

        public int CurrentPage { get; set; }
        public int QuantityPages { get; set; }

        public IList<Product> Products;

        public IndexModel(ILogger<IndexModel> logger, WebAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync([FromQuery(Name = "q")] string search, [FromQuery(Name = "s")] int? sequence = 1,
            [FromQuery(Name = "p")] int? page = 1)
        {
            this.CurrentPage = page.Value;

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    p => p.Name.ToUpper().Contains(search.ToUpper())).OrderBy(p => p.Name);
            }

            if (sequence.HasValue)
            {
                switch (sequence.Value)
                {
                    case 1:
                        query = query.OrderBy(p => p.Name.ToUpper());
                        break;
                    case 2:
                        query = query.OrderBy(p => p.Price);
                        break;
                    case 3:
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    default:
                        break;
                }
            }

            var queryCount = query;
            int productsCount = queryCount.Count();

            // Devolve o valor teto de um decimal
            this.QuantityPages = Convert.ToInt32(Math.Ceiling(productsCount * 1M / PageSize));

            query = query.Skip(PageSize * (this.CurrentPage - 1)).Take(PageSize);
            Products = await query.ToListAsync();
        }
    }
}
