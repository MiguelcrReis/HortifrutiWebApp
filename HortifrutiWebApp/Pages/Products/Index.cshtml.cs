using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebAppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IList<Product> Products { get; set; }

        public IndexModel(WebAppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

                if (await _context.SaveChangesAsync() > 0)
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                        "img\\product", product.ProductId.ToString("D6") + ".jpg");

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
