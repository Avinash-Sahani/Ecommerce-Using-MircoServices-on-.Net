using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery,List<GetOrderResponse>>
{
    private IOrderRepository Repository { get; }

    public GetOrdersListQueryHandler(IOrderRepository repository)
    {
        Repository = repository;

    }


    public async Task<List<GetOrderResponse>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = await Repository.GetOrdersByUsername(request.UserName);
        var getOrdersList = new GetOrdersList(orders).OrderList;
        return getOrdersList;
    }
}

  