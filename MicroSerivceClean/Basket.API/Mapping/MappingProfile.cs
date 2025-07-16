using AutoMapper;
using Basket.API.Model;
using Enventbus.Messages.Events;

namespace Basket.API.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile ()
        {
            CreateMap<BasketCheckoutEvent, BasketCheckout>().ReverseMap();
        }
    }
}
