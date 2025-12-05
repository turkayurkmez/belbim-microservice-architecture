using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.EventBus
{
    public record IntegrationEvent
    {
        public Guid Id { get; protected set; }
        public DateTime CreationDate { get; protected set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
