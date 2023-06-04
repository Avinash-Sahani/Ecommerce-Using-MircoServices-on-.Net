using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, IEnumerable<Order>>
{
    private IOrderRepository Repository { get; }

    public GetOrdersListQueryHandler(IOrderRepository repository)
    {
        Repository = repository;

    }
    
    public async Task<IEnumerable<Order>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        return await Repository.GetOrdersByUsername(request.UserName);
    }
}

  