using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class RestaurantDbContext
    : IdentityDbContext<ApplicationUser>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options) { }

        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<MenuItemImage> MenuItemImages => Set<MenuItemImage>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            
           // 1. حل مشكلة CartItems (الخطأ الحالي)
            builder.Entity<CartItem>()
                .HasOne(ci => ci.MenuItem)
                .WithMany()
                .HasForeignKey(ci => ci.MenuItemId)
                .OnDelete(DeleteBehavior.NoAction);

            // 2. حل مشكلة MenuItems (اللي حليناه المرة اللي فاتت)
            builder.Entity<MenuItem>()
                .HasOne(m => m.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // 3. تأمين الـ OrderItems (عشان متضربش معاك كمان شوية)
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany()
                .HasForeignKey(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.NoAction);

            // 4. ضبط أنواع الـ Decimal (عشان الـ Warnings تروح)
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }


        }
    }
    }
