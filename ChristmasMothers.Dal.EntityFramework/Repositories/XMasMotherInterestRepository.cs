using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChristmasMothers.Dal.Repositories;
using ChristmasMothers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChristmasMothers.Dal.EntityFramework.Repositories
{
    public class XMasMotherInterestJoinRepository : IXMasMotherInterestRepository
    {
        private readonly EntityFrameworkDbContext _context;
        public XMasMotherInterestJoinRepository(EntityFrameworkDbContext context)
        {
            _context = context;
        }

        public IEnumerable<XMasMotherInterestJoin> All()
        {
            return Query().ToArray();
        }

        public async Task<IEnumerable<XMasMotherInterestJoin>> AllAsync()
        {
            return await Query().ToArrayAsync();
        }

        public IQueryable<XMasMotherInterestJoin> Query()
        {
            return _context.Set<XMasMotherInterestJoin>();
        }

        public void Remove(XMasMotherInterestJoin entity)
        {
            _context.Remove(entity);
        }

        public async Task RemoveAsync(XMasMotherInterestJoin entity)
        {
            await Task.Run(() => _context.Remove(entity));
        }

        public async Task RemoveAsync(Guid xMasMotherId, Guid interestId)
        {
            var entity = await GetByIdAsync(xMasMotherId, interestId);
            if (entity != null)
            {
                await RemoveAsync(entity);

            }
        }

        public async Task<XMasMotherInterestJoin> GetByIdAsync(Guid xMasMotherId, Guid interestId)
        {
            return await _context.FindAsync<XMasMotherInterestJoin>(xMasMotherId, interestId);
        }

        public Task<XMasMotherInterestJoin> GetByIdWithChildrenAsync(Guid xMasMotherId, Guid interestId)
        {
            throw new NotImplementedException();
        }

        public void Add(XMasMotherInterestJoin entity)
        {
            _context.Add(entity);
        }

        public async Task AddAsync(XMasMotherInterestJoin entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        }

        public int SaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            return _context.SaveChanges(acceptAllChangesOnSuccess);
        }

        public int GetCount()
        {
            return Query().Count();
        }

        public async Task<int> GetCountAsync()
        {
            return await Query().CountAsync();
        }

        public async Task<ICollection<XMasMotherInterestJoin>> AllWithChildrenAsync()
        {
            var query = Query();
            foreach (var property in _context.Model.FindEntityType(typeof(XMasMotherInterestJoin)).GetNavigations())
            {
                query = query.Include(property.Name);
                await LoadRelativeXMasMotherrenAsync(property.GetTargetType().ClrType);
            }

            return await query.ToListAsync();
        }

        public XMasMotherInterestJoin GetById(Guid xMasMotherId, Guid interestId)
        {
            return _context.Find<XMasMotherInterestJoin>(xMasMotherId, interestId);
        }

        private async Task LoadRelativeXMasMotherrenAsync(Type entityTypeValue)
        {
            var setMethod = typeof(DbContext).GetMethod("Set");

            var entityType = _context.Model.FindEntityType(entityTypeValue);
            foreach (var navigation in entityType.GetNavigations())
            {
                await ((IQueryable)setMethod.MakeGenericMethod(navigation.GetTargetType().ClrType)
                    .Invoke(_context, null))
                    .OfType<object>()
                    .LoadAsync();
            }
        }
    }
}
