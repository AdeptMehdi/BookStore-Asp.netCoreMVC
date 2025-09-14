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
            // اضافه کردن Chapters
    public DbSet<ChapterModel> Chapters { get; set;} 

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

        public static class DbSeeder
        {
            public static void Seed(BookStore.Data.ApplicationDbContext context)
            {
                context.Database.EnsureCreated();

                // اگر جدول خالی بود، داده نمونه اضافه کن
                if (!context.Books.Any())
                {
                    var book1 = new BookModel
                    {
                        Title = "ASP.NET Core Guide",
                        Description = "Learn ASP.NET Core MVC step by step.",
                        Author = "Mehdi Afshari",
                        Price = 49.99m,
                        Quantity = 10,
                        ISBN = "1234567890",
                        ImagePath = "/images/default-book.jpg",
                    };

                    var book2 = new BookModel
                    {
                        Title = "C# Advanced Concepts",
                        Description = "Deep dive into C# advanced topics.",
                        Author = "John Doe",
                        Price = 59.99m,
                        Quantity = 8,
                        ISBN = "0987654321",
                        ImagePath = "/images/default-book.jpg",
                    };

                    context.Books.AddRange(book1, book2);
                    context.SaveChanges();

                    // اضافه کردن چند فصل نمونه
                    context.Chapters.AddRange(
                        new ChapterModel { BookId = book1.Id, Title = "Introduction", Content = "Welcome to ASP.NET Core." },
                        new ChapterModel { BookId = book1.Id, Title = "MVC Basics", Content = "Understanding MVC architecture." },
                        new ChapterModel { BookId = book2.Id, Title = "Advanced LINQ", Content = "Deep dive into LINQ queries." },
                        new ChapterModel { BookId = book2.Id, Title = "Async Programming", Content = "Working with async/await." }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}