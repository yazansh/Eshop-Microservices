using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountDbContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; }

    public DiscountDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            [
                new Coupon(){
                    Id = 1,
                    ProductName = "IPhone X",
                    Description = "IPhone X desc",
                    Amount = 100
                },
                new Coupon(){
                    Id = 2,
                    ProductName = "IPhone 11",
                    Description = "IPhone 11 desc",
                    Amount = 200
                }
            ]);
    }
}
