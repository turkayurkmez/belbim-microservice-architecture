using BelbimEShop.Catalog.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
        private readonly IProductRepository productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync();

            var productSummaries = products.Select(p => new ProductSummary(p.Id, p.Price, p.Description, p.ImageUrl, p.CategoryId));

            var response = new GetAllProductsResponse(productSummaries, productSummaries.Count());

            return response;

        }
    }
}
