using BelbimEShop.Shared.EventBus;
using MassTransit;

namespace BelbimEShop.Order.API.Consumers
{

    //0. Tüketici sınıfını oluşturun
    public class OrderProductPriceDiscountConsumer : IConsumer<ProductPriceDiscountedIntegrationEvent>
    {

        private readonly ILogger<OrderProductPriceDiscountConsumer> _logger;

        public OrderProductPriceDiscountConsumer(ILogger<OrderProductPriceDiscountConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ProductPriceDiscountedIntegrationEvent> context)
        {

            _logger.LogInformation($"BURASI ORDER SERVICE, Catalog Servisi ürün fiyatını güncelledi! {context.Message.ProductId} -> {context.Message.NewPrice}");
            // Here you can add logic to handle the price discount event, e.g., update orders, notify users, etc.
            return Task.CompletedTask;

        }
    }
}
