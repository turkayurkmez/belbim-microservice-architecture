using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.Library.Domain
{

    /*
     * Tüm domain olaylarını yöneten ve "aggregate" olarak tanımlanabilen her entity, bazı kuralları barındırmak zorunda.
     * O kuralları da bu abstract class'da tanımlıyoruz.
     */
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot where T : struct, IEquatable<T>
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents;

        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }

        public void AddDomainEvent(IDomainEvent domainEvent) { 
            domainEvents.Add(domainEvent);
        }

    }
}
