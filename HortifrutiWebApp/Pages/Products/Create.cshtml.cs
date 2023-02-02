using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }


        private readonly WebAppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public string ImagePath { get; set; }

        [BindProperty]
        [Display(Name = "Image Product")]
        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        public IFormFile ImageProduct { get; set; }

        public CreateModel(WebAppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            ImagePath = "~/img/product/no_image.jpg";
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageProduct == null)
            {
                return Page();
            }

            var product = new Product();

            if (await TryUpdateModelAsync(product, Product.GetType(), nameof(Product)))
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                await AppUtils.ProcessImageFile(product.ProductId, ImageProduct, _webHostEnvironment);

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
