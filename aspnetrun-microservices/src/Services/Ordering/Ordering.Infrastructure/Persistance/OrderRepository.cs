using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance;

public class OrderRepository : RepositoryBase<Order>,IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUsername(string username)
    {
        return await _dbContext.Orders.Where(order => (order.UserName ?? new string(string.Empty) ).Equals(username)).ToListAsync();
    }
}