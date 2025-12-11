using BelbimEShop.Basket.API.Protos;
using Grpc.Core;

namespace BelbimEShop.Basket.API.Services
{
    public class CustomBasketService : BasketService.BasketServiceBase
    {
        public override Task<BasketResponse> AddItem(AddItemRequest request, ServerCallContext context)
        {
            var response = new BasketResponse
            {
                UserId = request.UserId,
                Items = { new BasketItem { ProductId = request.Item.ProductId, Price = request.Item.Price, Quantity = request.Item.Quantity } },
                TotalPrice = request.Item.Price * request.Item.Quantity

            };

            return Task.FromResult(response);
        }

        public override Task<BasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
        {

            var response = new BasketResponse
            {
                UserId = request.UserId,
                Items =
                {
                    new BasketItem { ProductId = 1111, Price = 10.0, Quantity = 2 },
                    new BasketItem { ProductId = 2222, Price = 20.0, Quantity = 1 }
                },
                TotalPrice = 40.0
            };
            return Task.FromResult(response);

        }


        //Update:


        public override Task<BasketResponse> Update(UpdateBasketRequest request, ServerCallContext context)
        {
            var response = new BasketResponse
            {
                UserId = request.UserId,
                Items = { new BasketItem { ProductId = request.Items[0].ProductId, Price = request.Items[0].Price, Quantity = request.Items[0].Quantity } },
                TotalPrice = request.Items[0].Price * request.Items[0].Quantity
            };

            return Task.FromResult(response);
        }



    }
}
