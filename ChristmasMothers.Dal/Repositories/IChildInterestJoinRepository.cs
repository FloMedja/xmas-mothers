using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.Repositories
{
    public interface IChildInterestRepository 
    {
        IEnumerable<ChildInterestJoin> All();
        Task<IEnumerable<ChildInterestJoin>> AllAsync();
        IQueryable<ChildInterestJoin> Query();
        void Remove(ChildInterestJoin entity);

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        Task RemoveAsync(ChildInterestJoin entity);
        Task RemoveAsync(Guid childId,Guid interestId);
        ChildInterestJoin GetById(Guid childId, Guid interestId);
        Task<ChildInterestJoin> GetByIdAsync(Guid childId, Guid interestId);
        Task<ChildInterestJoin> GetByIdWithChildrenAsync(Guid childId, Guid interestId);
        void Add(ChildInterestJoin entity);
        Task AddAsync(ChildInterestJoin entity);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges(bool acceptAllChangesOnSuccess = true);
        int GetCount();
        Task<int> GetCountAsync();

        Task<ICollection<ChildInterestJoin>> AllWithChildrenAsync();
    }
}
