using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal
{
    /// <summary>
    ///     A generic interface for repositories with batch operation support.
    /// </summary>
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        IEnumerable<TEntity> All();

        Task<IEnumerable<TEntity>> AllAsync();

        IQueryable<TEntity> Query();

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        void Remove(TEntity entity);

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        Task RemoveAsync(TEntity entity);

        /// <summary>
        ///     Deletes the specified entities.
        /// </summary>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Deletes the specified entities.
        /// </summary>
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Gets the entity with specified ID.
        /// </summary>
        TEntity GetById(TKey id);

        /// <summary>
        ///     Asynchronously gets the entity with specified ID.
        /// </summary>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        ///     Inserts the specified entity into the table.
        /// </summary>
        void Add(TEntity entity);

        /// <summary>
        ///     Inserts the specified entities into the table.
        /// </summary>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Asynchronously Inserts the specified entity into the table.
        /// </summary>
        Task AddAsync(TEntity entity);

        /// <summary>
        ///     Asynchronously Inserts the specified entities into the table.
        /// </summary>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Marks the specified entity as updated.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        ///     Marks the specified entities as updated.
        /// </summary>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Marks the specified entity as updated.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        ///    Asynchronously Marks the specified entities as updated.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SaveChanges(bool acceptAllChangesOnSuccess = true);

        int GetCount();
        Task<int> GetCountAsync();
    }
}