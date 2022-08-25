using Discount.API.Models;

namespace Discount.API.Services
{
  public interface IDiscountRepository
  {
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
    Task<Coupon> GetDiscount(string productName);
    Task<bool> UpdateDiscount(Coupon coupon);
  }
}