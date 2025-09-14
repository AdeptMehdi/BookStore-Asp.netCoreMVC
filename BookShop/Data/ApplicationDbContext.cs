using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // جداول ما
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // جلوگیری از Cascade Delete برای امنیت بیشتر
            builder.Entity<OrderItemModel>()
                .HasOne(oi => oi.Book)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderItemModel>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}