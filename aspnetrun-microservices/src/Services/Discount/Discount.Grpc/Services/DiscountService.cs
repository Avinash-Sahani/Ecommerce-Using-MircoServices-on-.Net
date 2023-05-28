using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private IDiscountRepository DiscountRepository { get; }
    private ILogger<DiscountService> Logger { get; }
    
    private IMapper Mapper { get; }
    
    public DiscountService(IDiscountRepository discountRepository,IMapper mapper, ILogger<DiscountService> logger)
    {
        DiscountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));;
    }
    
    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await DiscountRepository.GetDiscount(request.ProductName);
        if (coupon.Id == -1)
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Discount Code for {request.ProductName} not found"));
        Logger.LogInformation($"Coupon Found for product {coupon.ProductName} with Amount of {coupon.Amount}");
        var mappedCoupon = Mapper.Map<CouponModel>(coupon);
        return mappedCoupon;
    }

    public async override Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = Mapper.Map<Coupon>(request.Model);
        var isCouponCreated =await DiscountRepository.CreateDiscount(coupon);
        Logger.LogInformation($"Coupon Created");

        return new CreateDiscountResponse() { Sucess = isCouponCreated };
    }

    public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var isCouponDeleted =await DiscountRepository.DeleteDiscount(request.ProductName);
        Logger.LogInformation($"Coupon Deleted");

        return new DeleteDiscountResponse() { Sucess = isCouponDeleted };
        
    }

    public async override Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = Mapper.Map<Coupon>(request.Model);
          var isCouponUpdated =await DiscountRepository.CreateDiscount(coupon);
          Logger.LogInformation($"Coupon Updated");

          return new UpdateDiscountResponse() { Sucess = isCouponUpdated };
    }
}
