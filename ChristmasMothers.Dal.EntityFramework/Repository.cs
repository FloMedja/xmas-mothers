using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.EntityFramework
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly EntityFrameworkDbContext _context;

        public Repository(EntityFrameworkDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _context.RemoveRange(entities));
        }

        public TEntity GetById(TKey id)
        {
            return _context.Find<TEntity>(id);
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public IEnumerable<TEntity> All()
        {
            return Query().ToArray();
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Query().ToArrayAsync();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() => _context.Remove(entity));
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _context.Update(entity));
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _context.UpdateRange(entities));
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            return _context.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<int> GetCountAsync()
        {
            return  await Query().CountAsync();
        }

        public int GetCount()
        {
            return Query().Count();
        }
    }
}