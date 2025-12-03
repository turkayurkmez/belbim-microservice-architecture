using BelbimEShop.Catalog.Domain.Events;
using BelbimEShop.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Domain.Aggregates
{
    public class Product : AggregateRoot<long>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int? Stock { get; private set; }

        public string? ImageUrl { get; private set; }

        public int? CategoryId { get; set; }

        public Product()
        {
                
        }

        public Product(string name, string description, decimal price, int? stock, string? ımageUrl, int? categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImageUrl = ımageUrl;
            CategoryId = categoryId;
        }

        public void ApplyDiscount(decimal discountRate) 
        {
            var oldPrice = Price;
            Price = Price * (1 - discountRate);

            //Burada olay fırlayacak!
            ProductPriceDiscountedDomainEvent @event = new ProductPriceDiscountedDomainEvent(productId: Id, oldPrice: oldPrice,newPrice:Price);
            AddDomainEvent(@event);

            //Ürün resmi değiştiğinde
            //Rating değiştiğinde
        }

    }
}
