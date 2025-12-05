using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.EventBus
{

    public record OrderItemInEvent(long ProductId, int Quantity, decimal Price)  ;

    public record OrderCreatedEvent(int OrderId, string CustomerId, List<OrderItemInEvent> OrderItems, string CreditCardInfo) : IntegrationEvent;


    public record StockAvailableCommand(int OrderId, string CustomerId, string CreditCardInfo, decimal? TotalPrice);
    public record StockAvailableEvent(StockAvailableCommand Command) : IntegrationEvent;

    public record StockUnavailableEvent(int OrderId, string Reason): IntegrationEvent;

    public record PaymentSuccesfulEvent(int OrderId): IntegrationEvent;
    public record PaymentFailedEvent(int OrderId, string Reason):  IntegrationEvent;



        
   
}
