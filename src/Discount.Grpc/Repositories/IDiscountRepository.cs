using Discount.Grpc.Models;

namespace Discount.Grpc.Services
{
  public interface IDiscountRepository
  {
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
    Task<Coupon> GetDiscount(string productName);
    Task<bool> UpdateDiscount(Coupon coupon);
  }
}
