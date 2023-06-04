using MediatR;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder;

public class CheckOutOrderCommand : IRequest<int>
{
    public CheckOutOrderCommand(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
    
}