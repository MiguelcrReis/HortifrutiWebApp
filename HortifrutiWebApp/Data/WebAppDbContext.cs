using HortifrutiWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Data
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
