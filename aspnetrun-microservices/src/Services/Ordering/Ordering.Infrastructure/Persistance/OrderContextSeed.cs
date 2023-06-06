using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance;

public class OrderContextSeed
{
    
    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new() {UserName = "Avi", FirstName = "Avinash", LastName = "Sahani", EmailAddress = "avinash@gmail.com", AddressLine = "Bahcelievler", Country = "Pakistan", TotalPrice = 350 }
        };
    }
}

