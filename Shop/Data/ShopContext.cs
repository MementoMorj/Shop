using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Purchase)
                .WithMany(p => p.Items)
                .HasForeignKey(pi => pi.PurchaseId);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Product)
                .WithMany()
                .HasForeignKey(pi => pi.ProductId);
        }
    }
}