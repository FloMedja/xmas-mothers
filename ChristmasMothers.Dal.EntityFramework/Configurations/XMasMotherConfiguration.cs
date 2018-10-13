using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class XMasMotherConfiguration : EntityGuidConfiguration<XMasMother>
    {
        public override void Configure(EntityTypeBuilder<XMasMother> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => x.XMasMotherTrackingId);
            builder.Property(x => x.FamilyName).IsRequired();
            builder.Property(x => x.GivenName).IsRequired();
            builder.Property(x => x.CellNumber).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.GiftDeliver).IsRequired().HasDefaultValue(false);

            builder.HasOne(x => x.Address).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}