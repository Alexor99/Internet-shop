using Microsoft.EntityFrameworkCore;
using sirena.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Models
{
    public class SqlContext : DbContext
    {
        public DbSet<Product> Products { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Size> Sizes { set; get; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Contact> Contacts { set; get; }

        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///Product Category
            modelBuilder.Entity<ProductCategory>()
                .HasKey(x => new { x.ProductId, x.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductCategory)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.ProductCategory)
                .HasForeignKey(x => x.CategoryId);

            ///Product Color
            modelBuilder.Entity<ProductColor>()
                .HasKey(x => new { x.ProductId, x.ColorId });

            modelBuilder.Entity<ProductColor>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductColor)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductColor>()
                .HasOne(x => x.Color)
                .WithMany(x => x.ProductColor)
                .HasForeignKey(x => x.ColorId);

            ///Product Size
            modelBuilder.Entity<ProductSize>()
                .HasKey(x => new { x.ProductId, x.SizeId });

            modelBuilder.Entity<ProductSize>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductSize)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductSize>()
                .HasOne(x => x.Size)
                .WithMany(x => x.ProductSize)
                .HasForeignKey(x => x.SizeId);
        }
    }
}
