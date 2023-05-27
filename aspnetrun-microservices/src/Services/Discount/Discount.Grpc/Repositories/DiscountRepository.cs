using Dapper;
using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories;

public class DiscountRepository : IDiscountRepository
{
    public DiscountRepository(IDbConnection connection, ILogger<DiscountRepository> logger)
    {
        Connection = connection;
        DbConnection = Connection?.DbConnection;
        Logger = logger;
    }

    private IDbConnection Connection { get; }

    private ILogger<DiscountRepository> Logger { get; }
    private NpgsqlConnection? DbConnection { get; }


    public async Task<Coupon> GetDiscount(string productName)
    {
        Coupon? coupon = null;
        try
        {
            coupon = await DbConnection.QueryFirstOrDefaultAsync<Coupon?>(
                "SELECT * FROM Coupon WHERE ProductName = @ProductName", new Coupon { ProductName = productName });
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message ?? string.Empty);
        }

        return coupon ?? Coupon.Default;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        var query = "INSERT INTO Coupon (ProductName,Description,Amount) VALUES (@ProductName, @Description,@Amount)";
        var parameters = new
        {
            ProdcutName = coupon.ProductName,
            coupon.Description, Amount = coupon.Description
        };
        var isQuerySuccefullyExecuted = await DbConnection.ExecuteAsync(query, coupon);
        return isQuerySuccefullyExecuted != 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        var query = "UPDATE Coupon SET ProductName=@ProductName,Description=@Description,Amount=@Amount  WHERE Id=@Id";
        var parameters = new
        {
            ProdcutName = coupon.ProductName,
            coupon.Description, Amount = coupon.Description, coupon.Id
        };
        var isQuerySuccefullyExecuted = await DbConnection.ExecuteAsync(query, coupon);
        return isQuerySuccefullyExecuted != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        var query = "DELETE FROM  Coupon  WHERE ProductName=@ProductName";
        var parameters = new
            { ProductName = productName };
        var isQuerySuccefullyExecuted = await DbConnection.ExecuteAsync(query, parameters);
        return isQuerySuccefullyExecuted != 0;
    }
}