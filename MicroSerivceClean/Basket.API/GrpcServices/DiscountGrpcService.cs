using Discount.GRPC.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        public DiscountProtoService.DiscountProtoServiceClient _discountGrpcService;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountGrpcService) 
        {
            _discountGrpcService = discountGrpcService;
        }
        public async Task<CouponRequest>GetDiscount(string productId)
        {
            var getDiscountData = new GetDiscountRequest() {ProductId = productId};
           return await _discountGrpcService.GetDiscountAsync(getDiscountData);
        }
    }
}
