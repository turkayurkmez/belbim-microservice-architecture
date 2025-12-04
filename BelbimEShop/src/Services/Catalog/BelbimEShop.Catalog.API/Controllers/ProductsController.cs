using BelbimEShop.Catalog.Application.Features.Product.Commands.DiscountProductPrice;
using BelbimEShop.Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BelbimEShop.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly DiscountProductPriceCommandRequestHandler _discountProductPriceCommandRequestHandler;

        public ProductsController(DiscountProductPriceCommandRequestHandler discountProductPriceCommandRequestHandler)
        {
            _discountProductPriceCommandRequestHandler = discountProductPriceCommandRequestHandler;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ApplyDiscount(DiscountProductPriceCommandRequest request)
        {
            //FakeProductRepository fakeProductRepository = new FakeProductRepository();
            //DiscountProductPriceCommandRequestHandler discountProductPriceCommandRequestHandler = new DiscountProductPriceCommandRequestHandler(fakeProductRepository);

            var response = await _discountProductPriceCommandRequestHandler.Handle(request);
            return Ok(response);
        }


    }
}
