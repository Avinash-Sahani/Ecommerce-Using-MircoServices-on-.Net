using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrderRequest
{
    public GetOrderRequest(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}