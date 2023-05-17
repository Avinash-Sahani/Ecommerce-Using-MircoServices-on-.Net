using System.Net;
using Discount.API.Entities;
using Discount.API.Localization;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;
[ApiController]
[Route(Localizable.DiscountRouteConstant)]
public class DiscountController : ControllerBase
{
    private IDiscountRepository Repository { get; set; }

    public DiscountController(IDiscountRepository repository)
    {
        Repository = repository;
    }

    [HttpGet("productName")]
    [ProducesResponseType(typeof(Coupon),(int) HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> GetDiscount([FromQuery] string productName)
    {
        return Ok(await Repository.GetDiscount(productName));
    }
    [HttpPost]
    [ProducesResponseType(typeof(Coupon),(int) HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await Repository.CreateDiscount(coupon));
    }

    [HttpPut]
    [ProducesResponseType(typeof(Coupon),(int) HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await Repository.UpdateDiscount(coupon));
    }

    [HttpDelete("productName")]
    [ProducesResponseType(typeof(bool),(int) HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteDiscount([FromQuery] string productName)
    {
        return Ok(await Repository.DeleteDiscount(productName));
    }


}