using MediatR;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery : IRequest<List<GetOrderResponse>>
{
    public string UserName { get; set; }

    public GetOrdersListQuery(string? userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));

    }
}