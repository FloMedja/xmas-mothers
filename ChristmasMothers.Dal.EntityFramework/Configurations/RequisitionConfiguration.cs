using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class RequisitionConfiguration : EntityGuidConfiguration<Requisition>
    {
        public override void Configure(EntityTypeBuilder<Requisition> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Id);
            builder.HasKey(x => x.Id);

            //builder.Property(p => p.Name).HasMaxLength(255).IsRequired();

            //builder.Property(x => x.DisplayEn).HasMaxLength(255).IsRequired();

            //builder.Property(x => x.DisplayFr).HasMaxLength(255).IsRequired();

            //builder.HasMany(c => c.Domains).WithOne(p => p.Tenant);

            //builder.HasMany(p => p.Applications).WithOne(p => p.Tenant);
        }
    }
}