using Npgsql;

namespace Discount.Grpc.Data;

public interface IDbConnection
{
    public IConfiguration Configuration { get; set; }
    public NpgsqlConnection? DbConnection { get; set; }
}