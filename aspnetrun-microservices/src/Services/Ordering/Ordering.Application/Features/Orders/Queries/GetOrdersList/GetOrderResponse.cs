using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrderResponse
{
    public GetOrderResponse(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}