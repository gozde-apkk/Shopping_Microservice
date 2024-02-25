using Microsoft.AspNetCore.Mvc;
using Ms.Web.Models;
using Ms.Web.Services.CouponService;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ms.Web.Controllers
{
    public class CouponController : Controller
    {

        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }   
        public async Task<IActionResult> Index()
        {
            List<CouponDto> list = new();
            ResponseDto response = await _couponService.GetAllCouponsAsync();
            if (response.IsSuccess)
            {
                TempData["success"] = "Coupon created successfully";
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _couponService.CreateCouponsAsync(couponDto);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["error"] = response.Message;
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete(int couponId)
        {

            ResponseDto response = await _couponService.GetCouponByIdAsync(couponId) ;
            if (response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted successfully";

                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CouponDto couponDto)
        {

            ResponseDto response = await _couponService.DeleteCouponsAsync(couponDto.CouponId);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["error"] = response.Message;
            }
            return View(couponDto);
        }

    }
}
