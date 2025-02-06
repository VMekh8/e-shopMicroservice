using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly DiscountContext _context;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(DiscountContext context, ILogger<DiscountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var discount = await _context.Coupons
            .FirstOrDefaultAsync(arg => arg.ProductName == request.ProductName);

        discount ??= new Coupon { ProductName = "No discount", Description = "N/A", Amount = 0 };

        _logger.LogInformation("Discount is received for ProductName: {productName}, Amount: {Amount}", request.ProductName, discount.Amount);

        var couponModel = discount.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>();

        if (coupon is null) 
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        _context.Coupons.Add(coupon);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created new discount for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        _context.Coupons.Update(coupon);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated discount for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _context.Coupons
            .FirstOrDefaultAsync(arg => arg.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));

        _context.Coupons.Remove(coupon);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted information about discount for Product: {ProductName}", coupon.ProductName);

        return new DeleteDiscountResponse { IsDeleted = true };
    }
}