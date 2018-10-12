using System;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.EntityFramework
{
    public abstract class EntityGuidConfiguration<TEntity> : EntityFrameworkEntityTypeConfiguration<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        
    }
}