using Ms.Web.Models;

namespace Ms.Web.Services.CouponService
{
    public interface ICouponService
    {
        Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponsAsync(int id);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto);
    }
}