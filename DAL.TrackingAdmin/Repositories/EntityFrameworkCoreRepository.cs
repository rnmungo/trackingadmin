using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.TrackingAdmin.Contracts;
using DAL.TrackingAdmin.Contracts;
using DAL.TrackingAdmin.Exceptions;

namespace DAL.TrackingAdmin.Repositories
{
    public class EntityFrameworkCoreRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        internal DbSet<T> _entities;

        private bool disposed = false;

        public EntityFrameworkCoreRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }

        public void Create(T entity)
        {
            try
            {
                Entities.Add(entity);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                await Entities.AddAsync(entity).AsTask();
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            try
            {
                Entities.AddRange(entities);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await Entities.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                Entities.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public IQueryable<T> GetAll(bool tracking = false)
        {
            try
            {
                if (tracking) return Entities;
                return Entities.AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task<IQueryable<T>> GetAllAsync(bool tracking = false)
        {
            try
            {
                if (tracking) return Entities;
                return Entities.AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool tracking = false)
        {
            try
            {
                if (tracking) return Entities.Where(expression);
                return Entities.AsNoTracking().Where(expression);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression, bool tracking = false)
        {
            try
            {
                if (tracking) return Entities.Where(expression);
                return Entities.AsNoTracking().Where(expression);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, string includes, bool tracking = false)
        {
            try
            {
                IQueryable<T> query = tracking ? Entities.Where(expression) : Entities.AsNoTracking().Where(expression);
                foreach (string include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include.Trim());
                }
                return query;
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression, string includes, bool tracking = false)
        {
            try
            {
                IQueryable<T> query = tracking ? Entities.Where(expression) : Entities.AsNoTracking().Where(expression);
                foreach (string include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include.Trim());
                }
                return query;
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public T GetById(object id)
        {
            try
            {
                return Entities.Find(id);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task<T> GetByIdAsync(object id)
        {
            try
            {
                return await Entities.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public void Update(T entity)
        {
            try
            {
                Entities.Update(entity);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                Entities.Update(entity);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
