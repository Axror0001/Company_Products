using Products.Dto;
using Products.Models;

namespace Products.Servies
{
    public interface ICompanyService
    {
        public  Task<List<Company>> GetAll();

        public Task<Company> GetById(int Id);

        public Task<List<Product>> GetByIdCompany(int Id);

        public Task<Company> Create(CompanyDto companny);

        public Task<Company> Update(Company companny);

        public void Delete(int Id);
    }
}
