using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

namespace Ordering.Infrastructure.EventBusConsumer;

public class BasketCheckOutConsumer : IConsumer<BasketCheckOutEvent>
{

    private readonly IMediator _mediator;
    private readonly ILogger<BasketCheckOutConsumer> _logger;

    public BasketCheckOutConsumer(IMediator mediator, ILogger<BasketCheckOutConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
    {

        var orderInfo = context.Message.Order;
        var orderCheckoutCommand = new CheckOutOrderCommand(orderInfo); 
        var result = await _mediator.Send(orderCheckoutCommand);
        _logger.LogInformation($"BasketCheckout Consumed Succesfully . new order id is {result}");
    }
}