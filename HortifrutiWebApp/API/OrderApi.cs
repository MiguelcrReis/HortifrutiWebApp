using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        #region Dependency Injection
        private readonly WebAppDbContext _context;
        public OrderAPIController(WebAppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Update Order Item
        [HttpPost]
        public async Task<JsonResult> UpdateOrderItem([FromForm] int? orderId, [FromForm] int? productId = 0, [FromForm] int? quantity = 0)
        {
            if ((!orderId.HasValue) || (productId <= 0) || (quantity <= 0)) return new JsonResult(false);

            var order = await _context.Orders.Include("OrderItems").Include("OrderItems.Product").FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                if (order.OrderStatus == Models.Enums.OrderStatus.Realized)
                {
                    var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

                    if (orderItem != null)
                    {
                        orderItem.Quantity = quantity.Value;

                        if (_context.SaveChanges() > 0)
                        {
                            decimal amount = order.OrderItems.Sum(oi => oi.ItemValue);
                            var item = order.OrderItems.Select(x => new { id = x.ProductId, q = x.Quantity, v = x.ItemValue })
                                .FirstOrDefault(oi => oi.id == productId);
                            var jsonResult = new JsonResult(new { v = amount, item });

                            return jsonResult;
                        }
                    }
                }
            }
            return new JsonResult(false);
        }
        #endregion

        #region Delete Order Item
        [HttpPost]
        public async Task<JsonResult> DeleteOrderItem([FromForm] int? orderId, [FromForm] int? productId = 0)
        {
            if ((!orderId.HasValue) || (productId <= 0)) return new JsonResult(false);

            var order = await _context.Orders.Include("OrderItems").FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                if (order.OrderStatus == Models.Enums.OrderStatus.Realized)
                {
                    var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

                    if (orderItem != null)
                    {
                        order.OrderItems.Remove(orderItem);

                        if (_context.SaveChanges() > 0)
                        {
                            decimal amount = order.OrderItems.Sum(oi => oi.ItemValue);
                            var jsonResult = new JsonResult(new { v = amount, id = productId });

                            return jsonResult;
                        }
                    }
                }
            }
            return new JsonResult(false);
        }
        #endregion
    }
}
