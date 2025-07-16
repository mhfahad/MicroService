using AutoMapper;
using Discount.GRPC.Models;
using Discount.GRPC.Protos;

namespace Discount.GRPC.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Coupon,CouponRequest>().ReverseMap();
        }
    }
}
