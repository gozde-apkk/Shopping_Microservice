using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ms.Services.CouponAPI.Data;
using Ms.Services.CouponAPI.Models;
using Ms.Services.CouponAPI.Models.Dto;

namespace Ms.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {

        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;


        public CouponController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public ResponseDto GetCouponById(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.FirstOrDefault(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj);
                if (obj == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDto>(obj); ;
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Create([FromBody] CouponDto couponDto)
        {
            try
            {
                _response.Result = _mapper.Map<CouponDto>(_db.Coupons.Add(_mapper.Map<Coupon>(couponDto)).Entity);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }


        [HttpPut]
        public ResponseDto Updat([FromBody] CouponDto couponDto)
        {
            try
            {
                _response.Result = _mapper.Map<CouponDto>(_db.Coupons.Update(_mapper.Map<Coupon>(couponDto)).Entity);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var obj = _db.Coupons.FirstOrDefault(u => u.CouponId == id);
                if (obj != null)
                {
                    _response.Result = _mapper.Map<CouponDto>(_db.Coupons.Remove(obj).Entity);
                    _db.SaveChanges();
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

    }
}
