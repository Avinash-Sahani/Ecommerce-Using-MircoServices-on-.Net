using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.API.Controllers;

public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{userName}",Name = "GetOrder")]
    [ProducesResponseType(typeof(IEnumerable<Order>),(int) HttpStatusCode.OK)]

    public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUsername([FromQuery] string username)
    {

        var query = new GetOrdersListQuery(username);
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }

    [HttpPost(Name = "CheckOuOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckOutOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateOrder")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
       await _mediator.Send(command);
       return NoContent(); 
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]

    public async Task<ActionResult> DeleteOrder([FromQuery] int id)
    {
        var deleteCommand = new DeleteOrderCommand(id);
        var result = await _mediator.Send(deleteCommand);
        return NoContent();
    }
}