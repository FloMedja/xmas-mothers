﻿using System;
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
    public class ChildInterestJoinRepository: IChildInterestRepository
    {
        private readonly EntityFrameworkDbContext _context;
        public ChildInterestJoinRepository(EntityFrameworkDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ChildInterestJoin> All()
        {
            return Query().ToArray();
        }

        public async Task<IEnumerable<ChildInterestJoin>> AllAsync()
        {
            return await Query().ToArrayAsync();
        }

        public IQueryable<ChildInterestJoin> Query()
        {
            return _context.Set<ChildInterestJoin>();
        }

        public void Remove(ChildInterestJoin entity)
        {
            _context.Remove(entity);
        }

        public async Task RemoveAsync(ChildInterestJoin entity)
        {
            await Task.Run(() => _context.Remove(entity));
        }

        public async Task RemoveAsync(Guid childId, Guid interestId)
        {
            var entity = await GetByIdAsync(childId,interestId);
            if (entity != null)
            {
                await RemoveAsync(entity);

            }
        }

        public async Task<ChildInterestJoin> GetByIdAsync(Guid childId, Guid interestId)
        {
            return await _context.FindAsync<ChildInterestJoin>(childId,interestId);
        }

        public Task<ChildInterestJoin> GetByIdWithChildrenAsync(Guid childId, Guid interestId)
        {
            throw new NotImplementedException();
        }

        public void Add(ChildInterestJoin entity)
        {
            _context.Add(entity);
        }

        public async Task AddAsync(ChildInterestJoin entity)
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

        public async  Task<ICollection<ChildInterestJoin>> AllWithChildrenAsync()
        {
            var query = Query();
            foreach (var property in _context.Model.FindEntityType(typeof(ChildInterestJoin)).GetNavigations())
            {
                query = query.Include(property.Name);
                await LoadRelativeChildrenAsync(property.GetTargetType().ClrType);
            }

            return await query.ToListAsync();
        }

        public ChildInterestJoin GetById(Guid childId, Guid interestId)
        {
            return _context.Find<ChildInterestJoin>(childId,interestId);
        }

        private async Task LoadRelativeChildrenAsync(Type entityTypeValue)
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
