using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Application.Features.Product.Queries.GetAllProducts
{
    public record GetAllProductsRequest(): IRequest<GetAllProductsResponse>;   

    public record GetAllProductsResponse(IEnumerable<ProductSummary> Products, int TotalCount);
    public record ProductSummary(long Id, decimal Price, string Description, string? ImageUrl, int? CategoryId);


   
}
