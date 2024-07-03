using Products.Dto;
using Products.Models;

namespace Products.Servies
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();

        public Task<Product> GetById(int Id);

        public Task<Product> Create(ProductDto products);

        public Task<Product> Update(Product products);

        public void Delete(int Id);
    }
}
