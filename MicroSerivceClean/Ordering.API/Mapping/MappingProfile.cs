using AutoMapper;
using Enventbus.Messages.Events;
using Ordering.Application.Features.Orders.Commends.CreateOrder;

namespace Ordering.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckoutEvent, CreateOrderCommand>().ReverseMap();
        }
    }
}
