using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Data
{
    public class WebAppDbContext : IdentityDbContext<AppUser>
    {
        #region Constructor
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        {
        }
        #endregion

        #region Model Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<Favorite>().HasKey(x => new { x.ClientId, x.ProductId });

            modelBuilder.Entity<Visit>().HasKey(x => new { x.ClientId, x.ProductId });

            // Restrições

            //Restringe a exclusão de clientes  que possuam  pedidos
            modelBuilder.Entity<Order>()
                .HasOne<Client>(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Exclui automaticamente os itens de um pedido quando um pedido é excluido
            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Restringe a exclusão de produtos que possuem itens de pedidos
            modelBuilder.Entity<OrderItem>()
                .HasOne<Product>(oi => oi.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        #endregion

        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Visit> Visits { get; set; }
        #endregion
    }
}
