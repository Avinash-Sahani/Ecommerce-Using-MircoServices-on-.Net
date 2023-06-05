using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand,Unit>
{
    public IOrderRepository Repository { get; }
    public ILogger<DeleteOrderHandler> Logger { get; }
//Unit Represent Empty Result
public DeleteOrderHandler(IOrderRepository repository, ILogger<DeleteOrderHandler> logger)
{
    Repository = repository;
    Logger = logger;
}

public async  Task<Unit>  Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
{
    var ordertoDelete =  await Repository.GetByIdAsync(command.Id);
    if (ordertoDelete == null)
        throw new NotFoundException(nameof(command), command.Id);
     
    await Repository.DeleteAsync(ordertoDelete!);
    Logger.LogError($"Order with Id : {command.Id} delete succesfully");
    return Unit.Value;
}
}