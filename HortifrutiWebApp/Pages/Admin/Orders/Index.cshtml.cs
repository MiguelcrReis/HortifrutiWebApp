using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly WebAppDbContext _context;

        public IndexModel(WebAppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders.Include("Client")
                .OrderByDescending(o => o.DateTimeOrder).ToListAsync();
        }

        public async Task<IActionResult> OnPostCancelOrderAsync(int? id)
        {
            if (!id.HasValue) return NotFound();

            var order = await _context.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();

            if (order != null)
            {
                order.OrderStatus = OrderStatus.Canceled;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteOrderAsync(int? id)
        {
            if (!id.HasValue) return NotFound();

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
