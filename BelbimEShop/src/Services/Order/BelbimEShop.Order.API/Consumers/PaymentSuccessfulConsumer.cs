using BelbimEShop.Shared.EventBus;
using MassTransit;

namespace BelbimEShop.Order.API.Consumers
{
    public class PaymentSuccessfulConsumer : IConsumer<PaymentSuccesfulEvent>
                               
    {

        private readonly ILogger<PaymentSuccessfulConsumer> _logger;
        public PaymentSuccessfulConsumer(ILogger<PaymentSuccessfulConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<PaymentSuccesfulEvent> context)
        {
            _logger.LogInformation($"Ödeme başarılı: {context.Message.OrderId}");
            return Task.CompletedTask;

        }

 
    }
}
