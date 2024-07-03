using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Products.Data;
using Products.Dto;
using Products.Models;
using Products.RedisCacheService;
using Products.Servies;
using System.Text;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly IProductService _dbContext;
        private readonly ProductCacheService _cache;
        private readonly CompanyCacheService _cacheCompany;
        public ProductController(IProductService dbContext, ProductCacheService cache, CompanyCacheService cacheCompany)
        {
            this._dbContext = dbContext;
            this._cache = cache;
            this._cacheCompany = cacheCompany;
        }




        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProduct()
        {
            var key = "CreateProduct";
            string selectCache;
            var produc = new List<Product>();
            var getCache =  _cache.GetProductCache(key);
            if(!string.IsNullOrEmpty(getCache))
            {
                produc = JsonConvert.DeserializeObject<List<Product>>(getCache);
            }
            else
            {
                produc = await _dbContext.GetAll();
                selectCache = JsonConvert.SerializeObject(produc);
                _cache.SaveProductCache(selectCache, key);
            }
            
            return Ok(produc);
        }



        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdProduct(int Id)
        {
            var result = _dbContext.GetById(Id);
            return Ok(result);
        }




        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateCompany([FromBody] ProductDto products)
        {
            /*var key = "CreateProduct";
            var produc = new List<Product>();
            string productsJson = JsonConvert.SerializeObject(products);
            _cache.SaveProductCache(key, productsJson);*/
            var result = await _dbContext.Create(products);
            return Ok(result);
        }


        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateCompany([FromBody] Product products)
        {
            var result = await _dbContext.Update(products);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public void DeleteCompany(int Id)
        {
            _dbContext.Delete(Id);
            //var key = "CreateProduct";
            //_cache.DeleteProductCache(key);

        }
    }
}
