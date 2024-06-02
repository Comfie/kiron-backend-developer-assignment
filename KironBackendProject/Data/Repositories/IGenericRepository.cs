using System.Linq.Expressions;

namespace KironBackendProject.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T objModel);
        Task AddRangeAsync(IEnumerable<T> objModel);
        T? GetId(int id);
        Task<T?> GetIdAsync(int id);
        T? Get(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        int Count();
        Task<int> CountAsync();
        void Update(T objModel);
        void Remove(T objModel);
        void Dispose();
    }

}
