using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountDbContext dbContext, ILogger<DiscountService> logger)
    : Discount.DiscountBase
{

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon()
        {
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = request.Coupon.Amount,
        };

        try
        {
            await dbContext.Coupons.AddAsync(coupon);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Exception: {{@Exception}}", ex);
            throw;
        }

        return new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount,
        };
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon;
        try
        {
            coupon = await dbContext.Coupons
                .Where(c => c.ProductName.Equals(request.ProductName))
                .FirstOrDefaultAsync() ?? throw new Exception($"Product name: {request.ProductName} was not found!");

        }
        catch (Exception ex)
        {
            logger.LogError($"Exception: {{@Exception}}", ex);
            throw;
        }
        return new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount,
        };
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon;
        try
        {
            coupon = await dbContext.Coupons
                .Where(c => c.ProductName.Equals(request.Coupon.ProductName))
                .FirstOrDefaultAsync() ?? throw new Exception($"Product name: {request.Coupon.ProductName} was not found!");

            coupon.Description = request.Coupon.Description;
            coupon.Amount = request.Coupon.Amount;

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Exception: {{@Exception}}", ex);
            throw;
        }

        return new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount,
        };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon;

        try
        {
            coupon = await dbContext.Coupons
                .Where(c => c.ProductName.Equals(request.ProductName))
                .FirstOrDefaultAsync() ?? throw new Exception($"Product name: {request.ProductName} was not found!");

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Exception: {{@Exception}}", ex);
            throw;
        }

        return new DeleteDiscountResponse { Success = true };
    }
}
