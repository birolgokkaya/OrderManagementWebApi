using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Repositories.Base;
using OrderManagement.Persistence.Contexts;
using System.Linq.Expressions;

namespace OrderManagement.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly OrderManagementDbContext _context;
        protected DbSet<TEntity> _entity;

        public Repository(OrderManagementDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public virtual bool Delete(TEntity entity)
        {
            _entity.Remove(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.Where(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual bool Update(TEntity entity)
        {
            _entity.Update(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}