using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "Apple iPhone 15", Description = "Iphone 15 discount", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Samsung Galaxy S23", Description = "Samsung discount", Amount = 100 }
        );
    }
}