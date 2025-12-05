using BelbimEShop.Catalog.Domain.Events;
using BelbimEShop.Shared.EventBus;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Infrastructure.EventHandlers
{
    public class DiscountProductPriceDomainEventHandler : INotificationHandler<ProductPriceDiscountedDomainEvent>
    {
        private readonly ILogger<DiscountProductPriceDomainEventHandler> _logger;
        private readonly IPublishEndpoint publishEndpoint;

        public DiscountProductPriceDomainEventHandler(ILogger<DiscountProductPriceDomainEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            this.publishEndpoint = publishEndpoint;
        }
        public Task Handle(ProductPriceDiscountedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Ürün fiyatı indirildi: {notification.ProductId}, Eski Fiyat: {notification.OldPrice}, Yeni Fiyat: {notification.NewPrice}");
          

            //Diğer mikroservislere bildirim gönderme işlemleri burada yapılacak....
            ProductPriceDiscountedIntegrationEvent integrationEvent = new ProductPriceDiscountedIntegrationEvent(notification.ProductId, notification.OldPrice, notification.NewPrice);
            //olayı yayınla:
            publishEndpoint.Publish(integrationEvent, cancellationToken);



            return Task.CompletedTask;
        }
    }
}
