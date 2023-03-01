using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using HortifrutiWebApp.Models.Enums;

namespace HortifrutiWebApp.Pages.Admin.OrderAdmin
{
    [Authorize(Policy = "isAdmin")]
    public class OrderDetailsModel : PageModel
    {
        private readonly WebAppDbContext _context;

        public OrderDetailsModel(WebAppDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task OnGetAsync([FromQuery] int? id)
        {
            if (id.HasValue)
            {
                Order = await _context.Orders.Include("Client").Include("OrderItems").Include("OrderItems.Product").Where(o => o.OrderId == id).FirstOrDefaultAsync();
            }
        }

        public async Task<IActionResult> OnPostTakeOrderAsync(int? id)
        {
            if (!id.HasValue) return NotFound();

            Order = await _context.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();

            if (Order != null)
            {
                Order.OrderStatus = OrderStatus.Answered;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Admin/Admin");
        }
    }
}
