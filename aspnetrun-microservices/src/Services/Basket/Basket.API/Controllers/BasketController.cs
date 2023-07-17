#nullable enable
using System.Net;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Catalog.API.Localization;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers;
[ApiController]
[Route(Localizable.BasketRouteConstant)]
public class BasketController : ControllerBase
{
    private IBasketRepository BasketRepository { get; }
    private DiscountGrpcService DiscountGrpcService { get; }

    private readonly IPublishEndpoint PublishEndpoint;
    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService,IPublishEndpoint endpoint)
    {
        BasketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        DiscountGrpcService = discountGrpcService ?? throw  new ArgumentNullException(nameof(discountGrpcService));
        PublishEndpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
    }

    
    [HttpGet("{username:required}")]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart?>> GetBasket(string username)
    {
        var basket = await GetBasketFromRepository(username);
        return Ok(basket ?? new ShoppingCart(username));
    }

    private async Task<ShoppingCart?> GetBasketFromRepository(string username)
    {
        var basket = await BasketRepository.GetBasket(username);
        return basket;
    }

    [HttpPut]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
    {
        async Task ApplyDiscountCoupons()
        {
           
            foreach (var shoppingCartItem in cart?.Items!)
            {
                var coupon = await DiscountGrpcService.GetDiscountByProductName(shoppingCartItem.ProductName);
                shoppingCartItem.Price -= coupon.Amount;
            }
        }
        
        return Ok(await BasketRepository.UpdateBasket(cart));
    }
   
    [HttpDelete("{username}")]
    [ProducesResponseType(typeof(bool),(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(bool),(int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<bool>> DeleteBasket(string username)
    {
        var isDeleted = await BasketRepository.DeleteBasket(username);
        return isDeleted ? Ok(isDeleted) : NotFound(isDeleted);
    }

 
    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckOutEvent basketCheckout)
    {
        var username = basketCheckout?.UserName;
        if (string.IsNullOrEmpty(username?.Trim()))
            throw new ArgumentException(nameof(username));
        var currentBasket = await GetBasketFromRepository(basketCheckout?.UserName ?? string.Empty);
        if (currentBasket == null)
            return BadRequest();
        
        PublishEndpoint?.Publish(currentBasket);
        await BasketRepository.DeleteBasket(username);
        return Accepted();
    }


}