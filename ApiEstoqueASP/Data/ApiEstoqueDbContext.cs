using ApiEstoqueASP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueASP.Data
{
    public class ApiEstoqueDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }



        public ApiEstoqueDbContext(DbContextOptions<ApiEstoqueDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            //builder.Entity<User>();
            
            
            builder.Entity<Product>()
                .HasOne(e => e.Supplier)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.SupplierId)
                .IsRequired();

            builder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Supplier)
                .HasForeignKey(e => e.SupplierId)
                .IsRequired();

            builder.Entity<OrderItem>()
                .HasOne(e => e.Product)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.ProductId)
                .IsRequired();

            //builder.Entity<Product>()
            //    .HasMany(e => e.OrderItems)
            //    .WithOne(e => e.Product);

            builder.Entity<OrderItem>()
                .HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.OrderId)
                .IsRequired(required: false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItem>()
                .HasOne(e => e.User)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.UserId)
                .IsRequired(required: true)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Order>()
                .HasOne(e => e.User)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.UserId)
                .IsRequired(required: true)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
