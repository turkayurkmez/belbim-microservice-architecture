using BelbimEShop.Shared.EventBus;
using MassTransit;

namespace BelbimEShop.Order.API.Consumers
{
    public class StockUnavailableConsumer : IConsumer<StockUnavailableEvent>
    {
        private readonly ILogger<StockUnavailableConsumer> _logger;

        public StockUnavailableConsumer(ILogger<StockUnavailableConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<StockUnavailableEvent> context)
        {
            _logger.LogWarning($"Uygun stok bulunamadı: {context.Message.OrderId} içindeki ürünlerin stoğu yetersiz.");
            return Task.CompletedTask;
        }
    }


    
}
