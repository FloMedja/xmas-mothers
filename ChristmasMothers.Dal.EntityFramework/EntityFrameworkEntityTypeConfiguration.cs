using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;
using ChristmasMothers.Extensions;

namespace ChristmasMothers.Dal.EntityFramework
{
    public abstract class EntityFrameworkEntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> 
        where TEntity : class, IEntity<TKey>
        where TKey : struct, IComparable<TKey>, IEquatable<TKey>
    {
        protected virtual string TableName => typeof(TEntity).Name;

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (!TableName.IsNullOrWhiteSpace())
            {
                builder.ToTable(TableName);
            }

            MapId(builder);
        }

        protected virtual void MapId(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}