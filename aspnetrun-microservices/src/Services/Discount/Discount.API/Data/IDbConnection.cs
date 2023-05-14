using Npgsql;

namespace Discount.API;

public interface IDbConnection
{
    public IConfiguration Configuration { get; set; }
    public NpgsqlConnection? DbConnection { get; set; }
}