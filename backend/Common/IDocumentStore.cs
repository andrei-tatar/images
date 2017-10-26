using System;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public interface IStore<T> where T : class, IEntity
    {
        Task Add(T item);
        Task Update(T item);
        Task Delete(Guid id);
        IQueryable<T> Items { get; }
    }
}