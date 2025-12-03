using BelbimEShop.Catalog.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Application.Services
{
    public interface IProductService
    {
        //Bu mikroservis, product ile ......... yapacak:

        void CreateNewProduct(Product product);
        void DiscountToPrice(long id, decimal rate);

        void UpdateStock(long id, decimal price);

        //Her yeni işlem bir fonksiyon olarak eklenmeli....
        //İki yıl sonra burada 35 tane fonksiyon olursa .......
    }
}
