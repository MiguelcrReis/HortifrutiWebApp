using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Data;

namespace HortifrutiWebApp.Pages.Admin.OrderAdmin
{
    public class OrderEditModel : PageModel
    {
        private readonly WebAppDbContext _context;

        [BindProperty]
        public Order Order { get; set; }

        public OrderEditModel(WebAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            Order = await _context.Orders.Include("Client").FirstOrDefaultAsync(o => o.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var orderStatus = Order.OrderStatus;
            var orderAddress = Order.Address;
            Order = _context.Orders.FirstOrDefault(p => p.OrderId == Order.OrderId);
            Order.OrderStatus = orderStatus;
            Order.Address = orderAddress;
            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistOrder(Order.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Order = await _context.Orders.Include("Client").FirstOrDefaultAsync(x => x.OrderId == Order.OrderId);
            return RedirectToPage("./Index");
        }

        private bool ExistOrder(int id)
        {
            return _context.Orders.Any(o => o.OrderId == id);
        }
    }
}