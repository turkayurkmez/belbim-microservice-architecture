using BelbimEShop.Catalog.Application.Features.Product.Commands.DiscountProductPrice;
using BelbimEShop.Catalog.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BelbimEShop.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //private readonly DiscountProductPriceCommandRequestHandler _discountProductPriceCommandRequestHandler;

        private readonly IMediator mediator;
        public ProductsController(IMediator mediator)
        {
           // _discountProductPriceCommandRequestHandler = discountProductPriceCommandRequestHandler; 
            this.mediator = mediator;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ApplyDiscount(DiscountProductPriceCommandRequest request)
        {
            //FakeProductRepository fakeProductRepository = new FakeProductRepository();
            //DiscountProductPriceCommandRequestHandler discountProductPriceCommandRequestHandler = new DiscountProductPriceCommandRequestHandler(fakeProductRepository);

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(request);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }

          
            return BadRequest(ModelState);
            
        }


    }
}
