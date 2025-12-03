using BelbimEShop.Catalog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Application.Features.Product.Commands.DiscountProductPrice
{
    public class DiscountProductPriceCommandRequestHandler
    {
        private readonly IProductRepository productRepository;

        public DiscountProductPriceCommandRequestHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<DiscountProductPriceCommandResponse> Handle(DiscountProductPriceCommandRequest request)
        {
            //şimdi db ile çalış......
            //hangi db?,
            //hangi orm teknolojisi?

            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                return new DiscountProductPriceCommandResponse(false, $"{request.ProductId} id'li ürün bulunamadı");
            }

            product.ApplyDiscount(request.DiscountRate);
            await productRepository.UpdateAsync(product);

            return new DiscountProductPriceCommandResponse(true, "Ürün fiyatı güncellendi");
        }
    }
}
