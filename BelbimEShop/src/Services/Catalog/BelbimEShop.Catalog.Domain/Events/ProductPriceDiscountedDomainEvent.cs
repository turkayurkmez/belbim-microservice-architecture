using BelbimEShop.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Domain.Events
{
    public class ProductPriceDiscountedDomainEvent : DomainEvent
    {
        public long ProductId { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }

        public ProductPriceDiscountedDomainEvent(long productId, decimal oldPrice, decimal newPrice)
        {
            ProductId = productId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }
}
