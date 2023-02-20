using HortifrutiWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingCartAPI : ControllerBase
    {
        private readonly WebAppDbContext _context;

        public ShoppingCartAPI(WebAppDbContext context)
        {
            _context = context;
        }

        #region Update Cart Item
        [HttpPost]
        public async Task<JsonResult> UpdateCartItem([FromForm] string cartId, [FromForm] int? productId = 0, [FromForm] int? quantity = 0)
        {
            if (string.IsNullOrEmpty(cartId) || (productId <= 0) || (quantity <= 0)) return new JsonResult(false);

            var order = await _context.Orders.Include("OrderItems").FirstOrDefaultAsync(p => p.CartId == cartId);

            if (order != null)
            {
                if (order.OrderStatus == Models.Enums.OrderStatus.ShoppingCart)
                {
                    var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

                    if (orderItem != null)
                    {
                        orderItem.Quantity = quantity.Value;

                        if (_context.SaveChanges() > 0)
                        {
                            decimal orderValue = order.OrderItems.Sum(oi => oi.ItemValue);
                            var item = order.OrderItems.Select(
                                x => new
                                {
                                    id = x.ProductId,
                                    q = x.Quantity,
                                    v = x.ItemValue
                                }).FirstOrDefault(oi => oi.id == productId);
                            var jsonRes = new JsonResult(new { v = orderValue, item });
                            return jsonRes;
                        }
                    }
                }
            }
            return new JsonResult(false);
        }
        #endregion

        #region Delete Cart Item
        [HttpPost]
        public async Task<JsonResult> DeleteCartItem([FromForm] string cartId, [FromForm] int? productId = 0)
        {
            if (string.IsNullOrEmpty(cartId) || (productId <= 0)) return new JsonResult(false);

            var order = await _context.Orders.Include("OrderItems").FirstOrDefaultAsync(p => p.CartId == cartId);

            if (order != null)
            {
                if (order.OrderStatus == Models.Enums.OrderStatus.ShoppingCart)
                {
                    var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

                    if (orderItem != null)
                    {
                        order.OrderItems.Remove(orderItem);

                        if (_context.SaveChanges() > 0)
                        {
                            decimal orderValue = order.OrderItems.Sum(oi => oi.ItemValue);
                            var jsonRes = new JsonResult(new { v = orderValue, id = productId });

                            return jsonRes;
                        }
                    }
                } 
            }
            return new JsonResult(false);
        }
        #endregion
    }
}
