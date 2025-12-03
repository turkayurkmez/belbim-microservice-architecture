using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.Library.Domain
{
    public abstract class Entity<T> where T: struct, IEquatable<T>
    {
        //Varlık, kendisine ait eşsiz bir kimliği olan ve bu sayede aynı türde diğer varlıklardan ayrılan nesnelerdir.
        public T Id { get; protected set; }

        public DateTime CreatedDate { get; init; }

        public DateTime? LastModifiedDate { get; protected set; }

        protected Entity()
        {
            CreatedDate = DateTime.UtcNow;
            Id = typeof(T) == typeof(Guid) ? (T)(object)Guid.NewGuid() : default!;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            //1. aynı referans:
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            Entity<T> that = (Entity<T>)obj;
            return that.Id.Equals(this.Id);

          
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return Id.GetHashCode();
        }

        public static bool operator == (Entity<T> left, Entity<T> right)
        {
            if (left is null && right is null) return true;

            if (left is null || right is null) return false;

            return left.Equals(right);
        }

        public static bool operator != (Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }





    }
}
