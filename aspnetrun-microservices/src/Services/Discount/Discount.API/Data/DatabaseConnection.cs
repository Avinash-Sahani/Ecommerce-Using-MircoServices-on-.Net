using Discount.API.Localization;
using Npgsql;

namespace Discount.API.Data;
 
public class DatabaseConnection : IDbConnection
{
    public IConfiguration Configuration { get; set; }
    
    public NpgsqlConnection? Connection { get; set; } 

    public DatabaseConnection(IConfiguration configuration)
    {
        Configuration = configuration;
        IntializeConnection();
    }

    private void IntializeConnection()
    {
        try
        {
            var connectionString = Configuration.GetValue<string>(Localizable.ConnectionString);
            Connection = new NpgsqlConnection(connectionString);
        }
        catch (Exception e)
        { 
            
           
        }
    }
}