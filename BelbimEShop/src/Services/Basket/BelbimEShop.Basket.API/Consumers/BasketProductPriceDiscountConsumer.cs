using BelbimEShop.Shared.EventBus;
using MassTransit;

namespace BelbimEShop.Basket.API.Consumers
{
    public class BasketProductPriceDiscountConsumer : IConsumer<ProductPriceDiscountedIntegrationEvent>
    {
        private readonly ILogger<BasketProductPriceDiscountConsumer> _logger;
        public BasketProductPriceDiscountConsumer(ILogger<BasketProductPriceDiscountConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ProductPriceDiscountedIntegrationEvent> context)
        {
            var incomingMessage = context.Message;

            _logger.LogInformation($"BURASI BASKET API! Catalog API'si tarafından fiyatta indirim yapıldı. {incomingMessage.ProductId} id'li ürünün yeni fiyatı, {incomingMessage.NewPrice}");

            return Task.CompletedTask;

        }
    }
}
