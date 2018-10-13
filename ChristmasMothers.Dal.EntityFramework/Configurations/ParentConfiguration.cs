using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class ParentConfiguration : EntityGuidConfiguration<Parent>
    {
        public override void Configure(EntityTypeBuilder<Parent> builder)
        {
            base.Configure(builder);

            
            builder.Property(x => x.FamilyName).IsRequired();
            builder.Property(x => x.GivenName).IsRequired();
            builder.Property(x => x.CellNumber).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.GiftDeliveryOption).IsRequired();

            builder.HasOne(x => x.Address).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}