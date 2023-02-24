using Microsoft.AspNetCore.Mvc.RazorPages;
using HortifrutiWebApp.Data;
using System.Collections.Generic;
using HortifrutiWebApp.Models.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HortifrutiWebApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HortifrutiWebApp.Pages
{
    public class AdminModel : PageModel
    {
        private readonly WebAppDbContext _context;

        public AdminModel(WebAppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }


        public async Task OnGetAsync()
        {
            Orders = await _context.Orders.Include("Client")
                .Where(o => o.OrderStatus == OrderStatus.Realized)
                .OrderBy(o => o.DateTimeOrder).ToListAsync();
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
            return RedirectToPage("/Admin/Admin");
        }
    }
}
