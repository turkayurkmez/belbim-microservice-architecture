using BelbimEShop.Shared.EventBus;
using MassTransit;

namespace BelbimEShop.Order.API.Consumers
{
    public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly ILogger<PaymentFailedConsumer> _logger;
        public PaymentFailedConsumer(ILogger<PaymentFailedConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            _logger.LogWarning($"Ödeme başarısız: {context.Message.OrderId}, Sebep: {context.Message.Reason}");
            return Task.CompletedTask;
        }
    }
}
