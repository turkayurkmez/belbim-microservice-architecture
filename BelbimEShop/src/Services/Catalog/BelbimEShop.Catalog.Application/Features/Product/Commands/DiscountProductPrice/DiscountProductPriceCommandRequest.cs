using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Application.Features.Product.Commands.DiscountProductPrice
{

    //DataTransferObject

    //1. İsteği tutan nesne:
    public record DiscountProductPriceCommandRequest(long ProductId, decimal DiscountRate) : IRequest<DiscountProductPriceCommandResponse>;

    //2. İşlendikten sonra yanıtı tutan nesne:

    public record DiscountProductPriceCommandResponse(bool IsSuccess, string Message );



    
}
