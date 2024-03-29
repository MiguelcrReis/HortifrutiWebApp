using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Pages
{
    public class ShoppingCartModel : PageModel
    {
        #region Dependency Injection
        private readonly WebAppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public ShoppingCartModel(WebAppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion

        #region Parameters
        public string COOKIE_NAME { get { return ".AspNetCore.CartId"; } }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        #endregion

        #region OnGet Async
        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.ContainsKey(COOKIE_NAME))
            {
                var cartId = Request.Cookies[COOKIE_NAME];
                Order = await _context.Orders.Include("OrderItems").Include("OrderItems.Product").FirstOrDefaultAsync(o => o.CartId == cartId);

                if (Order != null)
                {
                    if (Order.OrderStatus != OrderStatus.ShoppingCart)
                    {
                        Response.Cookies.Delete(COOKIE_NAME);
                        return RedirectToPage("/Index");
                    }

                    Amount = Order.OrderItems.Sum(x => Convert.ToDecimal(x.Quantity) * x.UnitaryValue);
                }
                else
                    Amount = 0;
            }
            else
            {
                Amount = 0;
                SetCartCookie();
            }
            return Page();
        }
        #endregion

        #region OnPost Add To Cart Async
        public async Task<IActionResult> OnPostAddToCartAsync(int? id, int qtd = 1)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                string cartId = null;

                if (Request.Cookies.ContainsKey(COOKIE_NAME))
                {
                    cartId = Request.Cookies[COOKIE_NAME];
                    Order = await _context.Orders.Include("OrderItems").Include("OrderItems.Product").FirstOrDefaultAsync(p => p.CartId == cartId);
                }
                else
                    cartId = SetCartCookie();

                if (Order == null)
                {
                    Order = new Order
                    {
                        CartId = cartId,
                        DateTimeOrder = DateTime.UtcNow,
                        OrderStatus = OrderStatus.ShoppingCart,
                        OrderItems = new List<OrderItem>()
                    };

                    AppUser appUser = _signInManager.IsSignedIn(User) ? await _userManager.GetUserAsync(User) : null;

                    if (appUser != null)
                    {
                        Client client = await _context.Clients.FirstOrDefaultAsync<Client>(c => c.Email.ToLower().Equals(appUser.Email.ToLower()));

                        if (client != null) 
                            Order.ClientId = client.ClientId;
                    }

                    _context.Orders.Add(Order);
                }

                var orderItem = Order.OrderItems.FirstOrDefault(oi => oi.ProductId == id);

                if (orderItem == null)
                {
                    Order.OrderItems.Add(new OrderItem
                    {
                        ProductId = id.Value,
                        Quantity = qtd,
                        UnitaryValue = product.Price.Value
                    });
                }
                else
                    orderItem.Quantity += qtd;

                if (_context.SaveChanges() <= 0)
                    ModelState.AddModelError("", "Ocorreu um erro ao tentar adicionar o item ao carrinho.");
            }

            Amount = Order.OrderItems.Sum(x => Convert.ToDecimal(x.Quantity) * x.UnitaryValue);

            return Page();
        }
#endregion

        #region Function - Set Cart Cookie
        public string SetCartCookie()
        {
            var cartId = Guid.NewGuid().ToString();

            var options = new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Path = "/",
                Expires = DateTime.UtcNow.AddDays(90),
                IsEssential = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None,
                HttpOnly = false // Necessário para acessar via ajax
            };

            Response.Cookies.Append(COOKIE_NAME, cartId, options);

            return cartId;
        }
        #endregion
    }
}