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
    }
}
