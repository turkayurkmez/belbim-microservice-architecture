using BelbimEShop.Shared.EventBus;
using MassTransit;

namespace BelbimEShop.Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public OrderCreatedEventConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;
            bool isStockAvailable = checkStock(message.OrderItems);
            if (isStockAvailable)
            {
                var total = message.OrderItems.Sum(x => x.Price * x.Quantity);
                var command = new StockAvailableCommand(message.OrderId, message.CustomerId, message.CreditCardInfo, total);
                var @event = new StockAvailableEvent(command);

                await publishEndpoint.Publish(@event);
            }
            else
            {
                var @event = new StockUnavailableEvent(message.OrderId, "Ürün stoğu yetersiz");
                await publishEndpoint.Publish(@event);
            }

           

        }

        private bool checkStock(List<OrderItemInEvent> orderItems)
        {
            return true;
        }
    }
}
