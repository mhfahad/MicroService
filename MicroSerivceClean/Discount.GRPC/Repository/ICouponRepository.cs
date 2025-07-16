using Discount.GRPC.Models;

namespace Discount.GRPC.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetDiscount(string productID);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productID);
    }
}
