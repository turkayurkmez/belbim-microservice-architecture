using BelbimEShop.Catalog.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        public void CreateNewProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DiscountToPrice(long id, decimal rate)
        {
            throw new NotImplementedException();
        }

        public void UpdateStock(long id, decimal price)
        {
            throw new NotImplementedException();
        }
    }
}
