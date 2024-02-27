using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ms.Services.ProductAPI.Data;
using Ms.Services.ProductAPI.Models;
using Ms.Services.ProductAPI.Models.Dto;

namespace Ms.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
   // [Authorize]
    public class ProductController : ControllerBase
    {

        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;


        public ProductController(AppDbContext db, IMapper mapper)
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
                IEnumerable<Product> objList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
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
        public ResponseDto GetProductById(int id)
        {
            try
            {
                Product obj = _db.Products.FirstOrDefault(u => u.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(obj);
                if (obj == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Product not found";
                }
                else
                {
                    _response.Result = _mapper.Map<ProductDto>(obj); ;
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }



        [HttpPost]
       // [Authorize(Roles ="ADMIN")]
        public ResponseDto Create([FromBody] ProductDto ProductDto)
        {
            try
            {
                _response.Result = _mapper.Map<ProductDto>(_db.Products.Add(_mapper.Map<Product>(ProductDto)).Entity);
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
        public ResponseDto Updat([FromBody] ProductDto ProductDto)
        {
            try
            {
                _response.Result = _mapper.Map<ProductDto>(_db.Products.Update(_mapper.Map<Product>(ProductDto)).Entity);
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
                var obj = _db.Products.FirstOrDefault(u => u.ProductId == id);
                if (obj != null)
                {
                    _response.Result = _mapper.Map<ProductDto>(_db.Products.Remove(obj).Entity);
                    _db.SaveChanges();
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Product not found";
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
