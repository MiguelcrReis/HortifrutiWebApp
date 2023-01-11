using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly WebAppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Product Product { get; set; }

        public string ImagePath { get; set; }

        [BindProperty]
        [Display(Name = "Image Product")]
        public IFormFile ImageProduct { get; set; }

        public EditModel(WebAppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }

            ImagePath = $"~/img/product/{Product.ProductId:D6}.jpg";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Se há uma imagem de produto submetida 
                if (ImageProduct != null)
                {
                    await AppUtils.ProcessImageFile(Product.ProductId, ImageProduct, _webHostEnvironment);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
