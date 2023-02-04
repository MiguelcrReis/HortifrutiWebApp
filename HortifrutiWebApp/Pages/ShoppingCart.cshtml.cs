using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private WebAppDbContext _context;
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private const string COOKIE_NAME = ".AspNetCore.CartId";

        public Order Order { get; set; }
        public decimal Amount { get; set; }

        public ShoppingCartModel(WebAppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.ContainsKey(COOKIE_NAME))
            {
                var cartId = Request.Cookies[COOKIE_NAME];
                Order = await _context.Orders.Include("OrderItems").Include("OrderItems.Product").FirstOrDefaultAsync(p => p.CartId == cartId);
                if (Order != null)
                    Amount = Order.OrderItems.Sum(x => Convert.ToDecimal(x.Quantity) * x.UnitaryValue);
                else
                    Amount = 0;
            }
            else SetCartCookie();

            return Page();
        }

        public string SetCartCookie()
        {
            var cartId = Guid.NewGuid().ToString();

            var options = new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Path = "/",
                Expires = DateTime.UtcNow.AddDays(90),
                IsEssential = true,
                Secure = false,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None,
                HttpOnly = false // Necessário para acessar via ajax
            };

            Response.Cookies.Append(COOKIE_NAME, cartId, options);

            return cartId;
        }

        public async Task<IActionResult> OnPostAsync()
        {

        }
    }
}
