using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Pages.Clients
{
    [Authorize(Policy = "isAdmin")]
    public class IndexModel : PageModel
    {
        #region Dependency Injection
        private readonly WebAppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(WebAppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        #endregion

        #region Variables
        public IList<Client> Clients { get; set; }
        public IList<string> EmailsAdmin { get; set; }
        #endregion

        #region OnGet Async
        public async Task OnGetAsync()
        {
            Clients = await _context.Clients.ToListAsync();
        }
        #endregion

        #region OnPost Delete Async
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null) { return NotFound(); }

            var client = await _context.Clients.FindAsync(id);

            if (client != null)
            {
                _context.Clients.Remove(client);

                if (await _context.SaveChangesAsync() > 0)
                {
                    AppUser user = await _userManager.FindByNameAsync(client.Email);

                    if (user != null) await _userManager.DeleteAsync(user);
                }
            }

            return RedirectToPage("./Index");
        }
        #endregion
    }
}
