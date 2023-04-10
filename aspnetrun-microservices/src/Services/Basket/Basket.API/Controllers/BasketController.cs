using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Catalog.API.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers;
[ApiController]
[Route(Localizable.BasketRouteConstant)]
public class BasketController : ControllerBase
{
    private IBasketRepository BasketRepository { get; }
    public BasketController(IBasketRepository basketRepository)
    {
        BasketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
    }

    
    [HttpGet("{username:required}")]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart?>> GetBasket(string username)
    {
       var basket =  await BasketRepository.GetBasket(username);
       return Ok(basket ?? new ShoppingCart(username));
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


    
    
    
}