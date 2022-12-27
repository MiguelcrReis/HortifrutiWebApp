using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly WebAppDbContext _context;
        public IList<Client> Clients { get; set; }

        public IndexModel(WebAppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Clients = await _context.Clients.ToListAsync();
        }
    }
}
