using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ShopDbContext : DataContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        DbSet<Category> Categories { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ////custum name dbo

            //modelBuilder.HasDefaultSchema("Log");

            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Product)
                .WithOne(s => s.Supplier)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Category>()
                .HasMany(s => s.Product)
                .WithOne(s => s.Category)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                .HasIndex(it => new { it.Name });
        }
    }
}
