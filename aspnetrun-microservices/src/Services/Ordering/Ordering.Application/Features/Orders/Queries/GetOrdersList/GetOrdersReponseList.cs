using System.Collections;
using MediatR;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersList 
{

    public List<GetOrderResponse> OrderList { get; } = new();

    public GetOrdersList(IEnumerable<Order> orders)
    {
        foreach (var order in orders)
        {
            OrderList.Add(new GetOrderResponse(order));
        }
    }
    
}