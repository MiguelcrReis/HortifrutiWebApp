using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace HortifrutiWebApp.Pages.Clients
{
    //[Authorize(Roles ="admin")]
    [Authorize(Policy = "isAdmin")]
    public class EditModel : PageModel
    {
        #region Dependency Injection
        private readonly HortifrutiWebApp.Data.WebAppDbContext _context;
        public EditModel(HortifrutiWebApp.Data.WebAppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Parameters
        [BindProperty]
        public Client Client { get; set; }
        #endregion

        #region OnGet Async
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client = await _context.Clients.FirstOrDefaultAsync(m => m.ClientId == id);

            if (Client == null)
            {
                return NotFound();
            }
            return Page();
        }
        #endregion

        #region OnPost Async
        public async Task<IActionResult> OnPostAsync()
        {
            // Protect Email and CPF
            var client = await _context.Clients.Select(x => new { x.ClientId, x.Email, x.Cpf }).FirstOrDefaultAsync();
            Client.Email = client.Email;
            Client.Cpf = client.Cpf;

            if (ModelState.Keys.Contains("Client.Email"))
            {
                ModelState["Client.Email"].Errors.Clear();
                ModelState.Remove("Client.Email");
            }
            if (ModelState.Keys.Contains("Client.Cpf"))
            {
                ModelState["Client.Cpf"].Errors.Clear();
                ModelState.Remove("Client.Cpf");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Client).State = EntityState.Modified;
            _context.Attach(Client.Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(Client.ClientId))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToPage("./Index");
        }
        #endregion

        #region Function - Client Exists
        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
        #endregion
    }
}