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
}