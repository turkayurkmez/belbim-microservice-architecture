using BelbimEShop.Shared.EventBus;
using MassTransit;

namespace BelbimEShop.Payment.API.Consumers
{
    public class StockAvailableEventConsumer : IConsumer<StockAvailableEvent>
    {
        private readonly IPublishEndpoint _endpoint;

        private readonly ILogger<StockAvailableEventConsumer> logger;

        public StockAvailableEventConsumer(IPublishEndpoint endpoint, ILogger<StockAvailableEventConsumer> logger)
        {
            _endpoint = endpoint;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<StockAvailableEvent> context)
        {

            var incomingEvent = context.Message;

            var paymentSuccess = false; // Simulate payment success

            if (paymentSuccess)
            {
                var paymentCompletedEvent = new PaymentSuccesfulEvent(incomingEvent.Command.OrderId);

                logger.LogInformation("PAYMENT: Ödeme alındı");

                
                await _endpoint.Publish(paymentCompletedEvent);
            }
            else
            {
                var paymentFailedEvent = new PaymentFailedEvent(incomingEvent.Command.OrderId, "Sipariş ödemesi başarısız oldu.");


                logger.LogInformation("PAYMENT: Ödeme alınamadı");
                await _endpoint.Publish(paymentFailedEvent);
            }
        }
    }
}
