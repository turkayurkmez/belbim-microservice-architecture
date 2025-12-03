using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.Library.Domain
{
    public interface IAggregateRoot
    {
        //Aggregate'ler bir ya da birden fazla entity ile var olurlar. Başka bir değişle, sistem üzerinde kendi kendilerini yönetebilirler.

        //Bir varlığın olay koleksiyonunu aggregate nesneler tutar.

        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
