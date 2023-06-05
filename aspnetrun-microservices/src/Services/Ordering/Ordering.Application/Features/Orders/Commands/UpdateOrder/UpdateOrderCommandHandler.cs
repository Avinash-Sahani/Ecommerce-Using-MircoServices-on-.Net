using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand,Unit>
{
    public IOrderRepository Repository { get; }
    public ILogger<UpdateOrderCommandHandler> Logger { get; }

    public UpdateOrderCommandHandler(IOrderRepository repository, ILogger<UpdateOrderCommandHandler> logger)
    {
        Repository = repository;
        Logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await Repository.GetByIdAsync(request.Order.Id);
        if (order == null)
            throw new NotFoundException(nameof(request.Order), request.Order.Id);
        await Repository.UpdateAsync(order);
        Logger.LogInformation($"Order with Id {order.Id} UpdatedSuccesfully");
        return Unit.Value;
        

    }
}