using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.Repositories
{
    public interface IXMasMotherInterestRepository
    {
        IEnumerable<XMasMotherInterestJoin> All();
        Task<IEnumerable<XMasMotherInterestJoin>> AllAsync();
        IQueryable<XMasMotherInterestJoin> Query();
        void Remove(XMasMotherInterestJoin entity);

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        Task RemoveAsync(XMasMotherInterestJoin entity);
        Task RemoveAsync(Guid xMasMotherId, Guid interestId);
        XMasMotherInterestJoin GetById(Guid xMasMotherId, Guid interestId);
        Task<XMasMotherInterestJoin> GetByIdAsync(Guid xMasMotherId, Guid interestId);
        Task<XMasMotherInterestJoin> GetByIdWithChildrenAsync(Guid xMasMotherId, Guid interestId);
        void Add(XMasMotherInterestJoin entity);
        Task AddAsync(XMasMotherInterestJoin entity);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges(bool acceptAllChangesOnSuccess = true);
        int GetCount();
        Task<int> GetCountAsync();

        Task<ICollection<XMasMotherInterestJoin>> AllWithChildrenAsync();
    }
}
