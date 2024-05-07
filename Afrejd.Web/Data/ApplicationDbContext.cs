using Afrejd.Web.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Afrejd.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ConfirmedOrder> ConfirmedOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<CustomerInfo>()
                .HasOne(c => c.User)
                .WithMany(u => u.CustomerInfo)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CustomerInfo)
                .WithMany(ci => ci.Orders)
                .HasForeignKey(o => o.CustomerInfoId)
                .IsRequired();
        }
    }
}
