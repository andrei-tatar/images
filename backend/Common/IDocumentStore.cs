using System;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public interface IStore<T, in TId> where T : class, IEntity<TId>
    {
        Task Add(T item);
        Task Update(T item);
        Task Delete(TId id);
        IQueryable<T> Items { get; }
    }
}