using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Products.Data;
using Products.Dto;
using Products.Models;
using Products.RedisCacheService;
using Products.Servies;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _dbContext;
        private readonly CompanyCacheService _cache;
        private readonly ProductCacheService _cacheProduct;
        public CompanyController (ICompanyService dbContext, CompanyCacheService cache, ProductCacheService cacheProduct)
        {
            this._dbContext = dbContext;
            this._cache = cache;
            this._cacheProduct = cacheProduct;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCompany()
        {
            var key = "Company";
            string selectCache;
            var companies = new List<Company>();
            var cacheGet = _cache.GetCompanyCache(key);
            if(cacheGet != null)
            {
                companies = JsonConvert.DeserializeObject<List<Company>>(cacheGet);
                
            }
            else
            {
                companies = await _dbContext.GetAll();
                selectCache = JsonConvert.SerializeObject(companies);
                _cache.SaveCompanyCache(key, selectCache);

            }
            
            return Ok(companies);
        }



        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdCompany(int Id)
        {
            var result = _dbContext.GetById(Id);
            return Ok(result);
        }


        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetIdProducts(int Id)
        {
            var result = await _dbContext.GetByIdCompany(Id);
            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyDto company)
        {
            /*var key = "Company";
            var companyJson = JsonConvert.SerializeObject(company);
            var companies = new List<Company>();
            _cache.SaveCompanyCache(key, companyJson);*/
            await _dbContext.Create(company);
            return Ok(company);
        }


        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody] Company companys)
        {
            var result = await _dbContext.Update(companys);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public void DeleteCompany(int Id)
        {
            //var key = "Company";
            //_cache.DeleteCompanyCache(key);
             _dbContext.Delete(Id);
        }
    }
}
