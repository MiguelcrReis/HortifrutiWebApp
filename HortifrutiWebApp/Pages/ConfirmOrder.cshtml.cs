using System;
using System.Linq;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Pages
{
    [Authorize(Roles = "client")]
    public class ConfirmOrderModel : PageModel
    {
        #region Dependency Injection
        private readonly WebAppDbContext _context;
        public ConfirmOrderModel(WebAppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Parameters
        public Order Order { get; set; }
        public Client Client { get; set; }
        public string COOKIE_NAME { get { return ".AspNetCore.CartId"; } }
        #endregion

        #region OnGet Async
        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.ContainsKey(COOKIE_NAME))
            {
                var cartId = Request.Cookies[COOKIE_NAME];

                Order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.CartId == cartId);

                if (Order != null)
                {
                    if ((Order.OrderItems != null) && Order.OrderItems.Count > 0)
                    {
                        if (Order.OrderStatus == OrderStatus.ShoppingCart)
                        {
                            Client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
                            Order.ClientId = Client.ClientId;
                            Order.Address = Client.Address;
                            Order.Amount = Order.OrderItems.Sum(x => Convert.ToDecimal(x.Quantity) * x.UnitaryValue);
                            await _context.SaveChangesAsync();
                            return Page();
                        }
                    }
                }
            }
            return RedirectToPage("/ShoppingCart");
        }
        #endregion
    }
}
