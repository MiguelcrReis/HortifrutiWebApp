using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models;

namespace HortifrutiWebApp.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly WebAppDbContext _context;

        public CreateModel(WebAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; }

        #region OnGet
        public IActionResult OnGet()
        {
            return Page();
        }
        #endregion

        #region OnPostAsync
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = new Client();

            if (await TryUpdateModelAsync<Client>(client, "client",
                obj => obj.Name, obj => obj.LastName, obj => obj.Birthday, obj => obj.Cpf, obj => obj.Email, obj => obj.Phone))
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
