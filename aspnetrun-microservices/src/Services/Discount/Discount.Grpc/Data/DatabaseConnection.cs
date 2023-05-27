using Discount.Grpc.Localization;
using Npgsql;

namespace Discount.Grpc.Data;

public class DatabaseConnection : IDbConnection
{
    public DatabaseConnection(IConfiguration configuration)
    {
        Configuration = configuration;
        IntializeConnection();
    }

    public IConfiguration Configuration { get; set; }

    public NpgsqlConnection? DbConnection { get; set; }

    private void IntializeConnection()
    {
        try
        {
            var connectionString = Configuration.GetValue<string>(Localizable.ConnectionString);
            DbConnection = new NpgsqlConnection(connectionString);
        }
        catch (Exception e)
        {
        }
    }
}