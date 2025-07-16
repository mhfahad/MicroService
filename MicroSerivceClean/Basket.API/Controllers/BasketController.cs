using AutoMapper;
using Basket.API.GrpcServices;
using Basket.API.Model;
using Basket.API.Repository;
using CoreApiResponse;
using Enventbus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        IBasketRepository _basketRepository;
        DiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;
        IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);
                return CustomResult("Basket data load successfully.", basket ?? new ShoppingCart(userName));
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {   //TODO: Communicate discount.grpc
                //calculate latest price
                //Create discount grpc service
                Stopwatch _timer = new Stopwatch();
                _timer.Start();
                foreach (var item in basket.Items)
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductId);
                    item.Price -= coupon.Amount;
                }
                _timer.Stop();
                Console.WriteLine( _timer.ElapsedMilliseconds);

                return CustomResult("Basket modified done.", await _basketRepository.UpdateBasket(basket));
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
                return CustomResult("Basket has been deleted.");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Checkout([FromBody] BasketCheckout checkout)
        {
            var basket = await _basketRepository.GetBasket(checkout.UserName);
            if (basket == null)
            {
                return CustomResult("Basket is empty.", HttpStatusCode.BadRequest);
            }

            //Send checkout event to RabbitMQ

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(checkout);
            eventMessage.TotalPrice = basket.TotalPrice;
            
            await _publishEndpoint.Publish(eventMessage);

            //Remove basket
            await _basketRepository.DeleteBasket(basket.UserName);
            return CustomResult("Order has been placed.");
        }

    }
}
