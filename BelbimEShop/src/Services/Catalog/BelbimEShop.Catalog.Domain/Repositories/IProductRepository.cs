using BelbimEShop.Catalog.Domain.Aggregates;
using BelbimEShop.Shared.Library.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product,long>
    {
        Task<IEnumerable<Product>> SearchByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}
