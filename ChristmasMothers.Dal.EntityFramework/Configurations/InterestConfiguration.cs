using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class InterestConfiguration : EntityGuidConfiguration<Interest>
    {
        public override void Configure(EntityTypeBuilder<Interest> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => x.Label);
           
        }
    }
}