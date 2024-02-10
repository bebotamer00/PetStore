using System.Linq.Expressions;

namespace PetStore.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> Add(T entity);
        Task<T> Update(int id, T entity);
        Task Delete(int id);
        Task<int> Count();
    }
}
