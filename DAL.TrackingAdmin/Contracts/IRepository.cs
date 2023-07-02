using System.Linq.Expressions;
using Domain.TrackingAdmin.Contracts;

namespace DAL.TrackingAdmin.Contracts
{
    public interface IRepository<T> : IDisposable where T : class, IBaseEntity
    {
        void Create(T entity);
        Task CreateAsync(T entity);
        void CreateRange(IEnumerable<T> entities);
        Task CreateRangeAsync(IEnumerable<T> entities);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void DeleteRange(IEnumerable<T> entities);
        IQueryable<T> GetAll(bool tracking);
        Task<IQueryable<T>> GetAllAsync(bool tracking);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool tracking = false);
        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression, bool tracking = false);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, string includes, bool tracking = false);
        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression, string includes, bool tracking = false);
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        void Update(T entity);
        Task UpdateAsync(T entity);
    }
}
