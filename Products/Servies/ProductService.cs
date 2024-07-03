using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Dto;
using Products.Models;

namespace Products.Servies
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;
        public ProductService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Product>> GetAll()
        {
            var result = await _dbContext.product.ToListAsync();
            return result;
        }

       
        public async  Task<Product> GetById(int Id)
        {
            
                var result = await _dbContext.product.FirstOrDefaultAsync(x => x.Id.Equals(Id));
            if(result == null)
            {
                return null;
            }
                return result;
              
        }
       
        
        public async Task<Product> Create(ProductDto products)
        {
            var result = new Product()
            {
                CompanyId = products.CompanyId,
                ProductName = products.ProductName,
                ProductBreand = products.ProductBreand,
                ProductModel = products.ProductModel
            };
            await _dbContext.product.AddAsync(result);
            await _dbContext.SaveChangesAsync();
            return result;
        }


        public async Task<Product> Update(Product products)
        {
            var result = new Product()
            {
                Id = products.Id,
                CompanyId = products.CompanyId,
                ProductName = products.ProductName,
                ProductBreand = products.ProductBreand,
                ProductModel = products.ProductModel
            };
            _dbContext.product.Update(result);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async void Delete(int Id)
        {
            var result = await _dbContext.product.FirstOrDefaultAsync(x => x.Id.Equals(Id));
             _dbContext.product.Remove(result);
            await _dbContext.SaveChangesAsync();

        }

        
    }
}
