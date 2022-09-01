using Dapper;
using Discount.Grpc.Models;
using Npgsql;

namespace Discount.Grpc.Services
{
  public class DiscountRepository : IDiscountRepository
  {
    private readonly IDatabaseSettings _databaseSettigns;

    public DiscountRepository(IDatabaseSettings databaseSettigns)
    {
      _databaseSettigns = databaseSettigns ?? throw new ArgumentNullException(nameof(databaseSettigns));
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
      using var connection = new NpgsqlConnection(_databaseSettigns.ConnectionString);

      var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

      if (coupon == null)
      {
        return new Coupon
        {
          ProductName = "No Discount",
          Amount = 0,
          Description = "No Discount Desc"
        };
      }

      return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
      using var connection = new NpgsqlConnection(_databaseSettigns.ConnectionString);

      var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
        new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

      if (affected == 0)
      {
        return false;
      }

      return true;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
      using var connection = new NpgsqlConnection(_databaseSettigns.ConnectionString);

      var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
        new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

      if (affected == 0)
      {
        return false;
      }

      return true;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
      using var connection = new NpgsqlConnection(_databaseSettigns.ConnectionString);

      var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
          new { ProductName = productName });

      if (affected == 0)
      {
        return false;
      }

      return true;
    }
  }
}
