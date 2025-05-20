using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Core.Specifications;

namespace Youth_Innovation_System.Core
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsyncWithSpec(ISpecification<T> spec);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
