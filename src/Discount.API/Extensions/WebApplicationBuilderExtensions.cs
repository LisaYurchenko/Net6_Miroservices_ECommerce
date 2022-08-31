using Npgsql;

namespace Discount.API.Extensions
{
  public static class WebApplicationBuilderExtensions
  {
    public static void MigrateDatabase<TContext>(this WebApplicationBuilder host, int? retry = 0)
    {
      int retryForAvailability = retry.Value;


      var services = host.Services;
      var configuration = host.Configuration;

      try
      {

        using var connection = new NpgsqlConnection
            (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        connection.Open();

        using var command = new NpgsqlCommand
        {
          Connection = connection
        };

        command.CommandText = "DROP TABLE IF EXISTS Coupon";
        command.ExecuteNonQuery();

        command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
        command.ExecuteNonQuery();

      }
      catch (NpgsqlException ex)
      {

        if (retryForAvailability < 50)
        {
          retryForAvailability++;
          System.Threading.Thread.Sleep(2000);
          MigrateDatabase<TContext>(host, retryForAvailability);
        }
      }
    }
  }
}
