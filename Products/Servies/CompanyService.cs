using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Dto;
using Products.Models;

namespace Products.Servies
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _dbContext;
        public CompanyService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task<List<Company>> GetAll()
        {
            var result = await _dbContext.company.ToListAsync();
            return result;
        }


        public async Task<Company> GetById(int Id)
        {
            var result = await _dbContext.company.FirstOrDefaultAsync(x => x.Id.Equals(Id));
            return result;
        }


        public async Task<List<Product>> GetByIdCompany(int Id)
        {
            var result = await _dbContext.product.Where(x => x.CompanyId.Equals(Id)).ToListAsync();
            return result;
        }


        public async Task<Company> Create(CompanyDto companny)
        {
            var result = new Company()
            {

                CompanyName = companny.CompanyName,
                Model = companny.Model,
                Breand = companny.Breand
            };
            await _dbContext.company.AddAsync(result);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Company> Update(Company companny)
        {
            var result = new Company()
            {
                Id = companny.Id,
                CompanyName = companny.CompanyName,
                Model = companny.Model,
                Breand= companny.Breand
            };
             _dbContext.company.Update(result);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async void Delete(int Id)
        {
            var result =  _dbContext.company.FirstOrDefault(x => x.Id.Equals(Id));
            _dbContext.company.Remove(result);
            await _dbContext.SaveChangesAsync();

        }

        
    }
}
