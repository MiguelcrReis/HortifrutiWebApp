using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HortifrutiWebApp.Pages.Clients
{
    [Authorize(Policy = "isAdmin")]
    public class CreateModel : PageModel
    {
        #region Dependency Injection
        private readonly WebAppDbContext _context;
        public CreateModel(WebAppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Parameters
        [BindProperty]
        public Client Client { get; set; }
        #endregion

        #region OnGet
        public IActionResult OnGet()
        {
            return Page();
        }
        #endregion

        #region OnPost Async
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = new Client();
            client.Address = new Address();
            client.ClientStatus = ClientStatus.Registered;

            if (await TryUpdateModelAsync(client, Client.GetType(), nameof(Client)))
            {
                _context.Clients.Add(Client);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
        #endregion
    }
}
