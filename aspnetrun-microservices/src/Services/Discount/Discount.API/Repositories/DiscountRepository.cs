using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private IDbConnection Connection { get; }

    private ILogger<DiscountRepository> Logger { get; set; }
    private NpgsqlConnection? DbConnection { get; set; }
    
    public DiscountRepository(IDbConnection connection,ILogger<DiscountRepository> logger)
    {
        Connection = connection;
        DbConnection = Connection?.DbConnection;
        Logger = logger;
    }
    
    
    public async Task<Coupon> GetDiscount(string productName)
    {
        Coupon? coupon = null;
        try
        {
             coupon = await DbConnection.QueryFirstOrDefaultAsync<Coupon?>(
                $"SELECT * FROM Coupon WHERE ProductName = @ProductName", new Coupon() { ProductName = productName });
                
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message ?? string.Empty);
        }

        return coupon ??= Coupon.Default;
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