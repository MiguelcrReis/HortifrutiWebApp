using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Models.Enums;
using HortifrutiWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Pages
{
    [Authorize(Roles = "client")]
    public class FinalizeOrderModel : PageModel
    {
        private readonly WebAppDbContext _context;
        private IEmailSender _emailSender;
        public string COOKIE_NAME { get { return ".AspNetCore.CartId"; } }
        public Order Order { get; set; }
        public Client Client { get; set; }

        public FinalizeOrderModel(WebAppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.ContainsKey(COOKIE_NAME))
            {
                var cartId = Request.Cookies[COOKIE_NAME];

                Order = await _context.Orders.Include("OrderItems").Include("OrderItems.Product").FirstOrDefaultAsync(o => o.CartId == cartId);

                Client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);

                if ((Order.ClientId > 0) && (Order.Address != null))
                {
                    Order.OrderStatus = OrderStatus.Realized;
                    Order.DateTimeOrder = DateTime.Now;

                    foreach (var item in Order.OrderItems)
                    {
                        item.Product.Stock -= (int)item.Quantity;
                    }

                    await _context.SaveChangesAsync();
                    Response.Cookies.Delete(COOKIE_NAME);
                    await SendOrderSummaryEmail();
                    return Page();
                }
                else
                {
                    return RedirectToPage("/ConfirmOrder");
                }
            }
            return RedirectToPage("/ShoppingCart");
        }

        private async Task SendOrderSummaryEmail()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<p>Olá, {Client.Name}!</p>");
            sb.Append($"<p>Recebemos o seu pedido de compra número {Order.OrderId:D6}, " +
                $"no valor total de <b>{Order.Amount:C}");
            sb.Append($"<table border='1'><tr><th>Produto</th><th>Quantidade</th><th>Valor Unitário</th><th>Valor do Item</th></tr>");
            foreach (var item in Order.OrderItems)
            {
                sb.Append($"<tr><td>{item.Product.Name}</td><td>{item.Quantity}</td>" +
                    $"<td>R$ {item.UnitaryValue:F2}</td><td>R$ {item.ItemValue:F2}</td></tr>");
            }
            sb.Append($"<tr><td colspan='3'>Valor Total</td><td>{Order.Amount:C}</td></tr></table>");
            sb.Append($"<p>Você receberá em breve seus produtos no endereço a seguir: </p>");
            sb.Append($"<p>{Order.Address.Street}, {Order.Address.Number}, {Order.Address.Neighborhood}<br>" +
                $"Cidade: {Order.Address.City}-{Order.Address.State}<br>" +
                $"CEP: {Order.Address.Cep.Insert(5, "-").Insert(2, ".")}<br" +
                $"Complemento: {Order.Address.Complement}<br" +
                $"Referência: {Order.Address.Reference}");
            sb.Append($"<p>Agradecemos a sua preferência e confiança.</p>");
            sb.Append($"<p>Atenciosamente, <br>Equipe HortiFruti Reis.</p>");

            try
            {
                await _emailSender.SendEmailAsync(Client.Email, $"Pedido {Order.OrderId:D6}", "", sb.ToString());
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Erro ao enviar e-mail: {e.Message}");
            }
        }
    }
}
