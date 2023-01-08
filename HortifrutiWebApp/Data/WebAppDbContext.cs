﻿using HortifrutiWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HortifrutiWebApp.Data
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<Favorite>().HasKey(x => new { x.ClientId, x.ProductId });

            modelBuilder.Entity<Visit>().HasKey(x => new { x.ClientId, x.ProductId });
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Client> Clients { get; set; }  
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
