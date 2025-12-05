using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.EventBus
{
    /*
     * Bu olay, catalog mikroservisinde bir ürünün fiyatında indirim yapıldığında yayınlanır.
     * Alıcıları, sepet mikroservisi ve sipariş mikroservisidir.
     */
    public record ProductPriceDiscountedIntegrationEvent(long ProductId, decimal OldPrice, decimal NewPrice):IntegrationEvent;
    
}
