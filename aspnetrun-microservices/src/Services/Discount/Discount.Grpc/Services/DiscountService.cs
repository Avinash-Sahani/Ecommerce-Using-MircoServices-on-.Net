using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private IDiscountRepository DiscountRepository { get; }
    private ILogger<DiscountService> Logger { get; }
    
    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
    {
        DiscountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));;
    }

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);
    }

    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await DiscountRepository.GetDiscount(request.ProductName);
        if (coupon.Id == -1)
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Discount Code for {request.ProductName} not found"));
        return coupon;
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }
}
