using MediatR;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<Unit>
{
    public UpdateOrderCommand(Order order)
    {
        Order = order;
    }

    public Order Order { get; }

}