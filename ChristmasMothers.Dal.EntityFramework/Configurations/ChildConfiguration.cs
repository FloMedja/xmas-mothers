using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class ChildConfiguration : EntityGuidConfiguration<Child>
    {
        public override void Configure(EntityTypeBuilder<Child> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => x.ChildTrackingId);
            builder.Property(x => x.FamilyName).IsRequired();
            builder.Property(x => x.GivenName).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.Parent).IsRequired();
            builder.Property(x => x.Age).IsRequired();
            builder.Property(x => x.Sexe).IsRequired();

            builder.HasOne(x => x.Address).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Parent).WithMany(p => p.Children).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.GiftGiver).WithMany(xmm => xmm.MatchedChildren).HasForeignKey(x => x.GiftGiverId).OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}