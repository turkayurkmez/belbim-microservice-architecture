using BelbimEShop.Catalog.Domain.Aggregates;
using BelbimEShop.Catalog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Infrastructure.Repositories
{
    public class FakeProductRepository : IProductRepository
    {


        public Task<Product> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            List<Product> products = new List<Product>()
            {
                new Product("Ürün A","Açıklama 1",1,1,null,1),
                new Product("Ürün B","Açıklama 1",1,1,null,1)

            };
            return await Task.FromResult(products);
        }

        public async Task<Product> GetByIdAsync(long id)
        {
            var product = new Product("Ürün A", "Açıklama 1", 1, 1, null, 1);
            return await Task.FromResult(product);

        }

        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
          return await Task.FromResult(entity);
        }
    }
}
