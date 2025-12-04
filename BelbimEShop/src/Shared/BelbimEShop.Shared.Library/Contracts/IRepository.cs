using BelbimEShop.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Shared.Library.Contracts
{
    public interface IRepository<T, TId> where T : IAggregateRoot
                                         where TId : struct, IEquatable<TId>
    {
        Task<IEnumerable<T>> GetAllAsync();

       // Task<IEnumerable<T>> GetAllAsync(int pageNo, int size);

        Task<T> GetByIdAsync(TId id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(TId id);



    }
}
