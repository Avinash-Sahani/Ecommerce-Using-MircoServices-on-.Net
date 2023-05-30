using Discount.Grpc.Data;
using Npgsql;

namespace Discount.Grpc;

public static class HostExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host,int? retry=0)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                logger.LogInformation("Migrating postgres sql database started");
                var connection = new DatabaseConnection(configuration);
                var dbConnection = connection?.DbConnection;
                dbConnection?.Open();
                var createTableQuery =
                    @"Create Table IF NOT EXISTS Coupon(Id SERIAL PRIMARY KEY,ProductName VARCHAR(24) NOT NULL,Description TEXT , Amount INT)";
                ExecuteSqlQuery(dbConnection, createTableQuery);
                var insertDummayData = 
                    $@"INSERT INTO Coupon (ProductName, Description, Amount)
SELECT 'IPhone X', 'IPhone X Description', 1500
WHERE NOT EXISTS (
    SELECT 1 FROM Coupon
    WHERE ProductName = 'IPhone X'
);
";
                ExecuteSqlQuery(dbConnection,insertDummayData);
                logger.LogInformation("Migration Succesful");
            }
            
            catch (NpgsqlException e)
            {
               logger.LogError("An error has occured "+e.Message);

               if (retry < 50)
               {
                   retry++;
                   System.Threading.Thread.Sleep(2000);
                   MigrateDatabase<TContext>(host, retry);
               }
            }

            return host;
        }
        
    }

    private static void ExecuteSqlQuery(NpgsqlConnection? connection, string query)
    {
   
        var command = new NpgsqlCommand() { Connection = connection };
        command.CommandText = query.Trim();
        command.ExecuteNonQuery();
    }
    
}