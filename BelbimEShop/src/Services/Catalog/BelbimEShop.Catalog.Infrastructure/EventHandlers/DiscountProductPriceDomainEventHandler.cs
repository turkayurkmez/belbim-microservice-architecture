using BelbimEShop.Catalog.Domain.Events;
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

        public DiscountProductPriceDomainEventHandler(ILogger<DiscountProductPriceDomainEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ProductPriceDiscountedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Ürün fiyatı indirildi: {notification.ProductId}, Eski Fiyat: {notification.OldPrice}, Yeni Fiyat: {notification.NewPrice}");
            return Task.CompletedTask;

            //Diğer mikroservislere bildirim gönderme işlemleri burada yapılacak....

        }
    }
}
