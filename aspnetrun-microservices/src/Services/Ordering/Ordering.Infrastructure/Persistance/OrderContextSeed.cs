using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
        }
    }
    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new() {UserName = "Avi", FirstName = "Avinash", LastName = "Sahani", EmailAddress = "avinash@gmail.com", AddressLine = "Bahcelievler", Country = "Pakistan", TotalPrice = 350 }
        };
    }
}

