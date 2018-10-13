using ChristmasMothers.Entities;
using ChristmasMothers.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class XMasMotherInterestJoinConfiguration
    {
        protected virtual string TableName => typeof(XMasMotherInterestJoin).Name;
        public void Configure(EntityTypeBuilder<XMasMotherInterestJoin> builder)
        {
            if (!TableName.IsNullOrWhiteSpace())
            {
                builder.ToTable(TableName);
            }

            builder.HasKey(x => new { x.XMasMotherId, x.InterestId });
            builder.HasOne(x => x.XMasMother).WithMany(xmm => xmm.XMasMotherInterests).HasForeignKey(x =>x.XMasMotherId);
            builder.HasOne(x => x.Interest).WithMany(i => i.InterestedXMasMothers).HasForeignKey(x => x.InterestId);

        }
    }
}
