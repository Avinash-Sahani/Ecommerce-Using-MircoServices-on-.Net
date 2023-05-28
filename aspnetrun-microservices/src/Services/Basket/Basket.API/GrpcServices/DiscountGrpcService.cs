using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;
    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        _discountProtoService = discountProtoService;
    }

    public async Task<CouponModel> GetDiscountByProductName(string productName)
    {
        var discountRequest = new GetDiscountRequest() { ProductName = productName };
        var couponModel = await _discountProtoService.GetDiscountAsync(discountRequest);
        return couponModel;
    }


}