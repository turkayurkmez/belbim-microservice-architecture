using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.Library.Domain
{
    public abstract class AggregateRoot<T> :Entity<T>, IAggregateRoot where T: struct, IEquatable<T>
    {

    }
}
