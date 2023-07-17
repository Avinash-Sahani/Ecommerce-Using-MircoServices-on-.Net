#nullable enable
using System.Net;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Catalog.API.Localization;
using EventBus.Messages.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers;
[ApiController]
[Route(Localizable.BasketRouteConstant)]
public class BasketController : ControllerBase
{
    private IBasketRepository BasketRepository { get; }
    private DiscountGrpcService DiscountGrpcService { get; }
    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        BasketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        DiscountGrpcService = discountGrpcService ?? throw  new ArgumentNullException(nameof(discountGrpcService));
    }

    
    [HttpGet("{username:required}")]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart?>> GetBasket(string username)
    {
       var basket =  await BasketRepository.GetBasket(username);
       await ApplyDiscountCoupons();
       return Ok(basket ?? new ShoppingCart(username));

        async Task ApplyDiscountCoupons()
       {
           if(basket == null) return;
           foreach (var shoppingCartItem in basket?.Items!)
           {
               var coupon = await DiscountGrpcService.GetDiscountByProductName(shoppingCartItem.ProductName);
               shoppingCartItem.Price -= coupon.Amount;
           }
       }
    }

    [HttpPut]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
    {
        
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

  //  public async Task<IActionResult> Checkout([FromBody] BasketCheck a)


}