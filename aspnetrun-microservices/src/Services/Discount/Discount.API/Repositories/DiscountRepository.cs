using Discount.API.Entities;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private IDbConnection Connection { get; }

    public DiscountRepository(IDbConnection connection)
    {
        Connection = connection;
    }
    
    public async Task<Coupon> GetDiscount(string productName)
    {
        throw new NotImplementedException();
    }

    public async  Task<bool> CreateDiscount(Coupon coupon)
    {
        throw new NotImplementedException();
    }

    public async  Task<bool> UpdateDiscount(Coupon coupon)
    {
        throw new NotImplementedException();
    }

    public async  Task<bool> DeleteDiscount(string productName)
    {
        throw new NotImplementedException();
    }
}