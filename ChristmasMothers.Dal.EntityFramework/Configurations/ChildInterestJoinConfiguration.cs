using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;
using ChristmasMothers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class ChildInterestJoinConfiguration 
    {
        protected virtual string TableName => typeof(ChildInterestJoin).Name;
        public void Configure(EntityTypeBuilder<ChildInterestJoin> builder)
        {
            if (!TableName.IsNullOrWhiteSpace())
            {
                builder.ToTable(TableName);
            }

            builder.HasKey(x => new {x.ChildId, x.InterestId});

            builder.HasOne(x => x.Child).WithMany(c => c.ChildInterests).HasForeignKey(x => x.ChildId);
            builder.HasOne(x => x.Interest).WithMany(i => i.InterestedChildren).HasForeignKey(x => x.InterestId);

        }
    }
}