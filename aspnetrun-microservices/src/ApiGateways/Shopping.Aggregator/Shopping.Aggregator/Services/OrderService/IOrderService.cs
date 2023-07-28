using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponse>> GetOrdersByUserName(string userName);
}