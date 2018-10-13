using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChristmasMothers.Entities;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class AddressConfiguration : EntityGuidConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Country).IsRequired();
            builder.Property(x => x.PostalCode).IsRequired();
            builder.Property(x => x.StreetName).IsRequired();
            builder.Property(x => x.Province).IsRequired();
            builder.Property(x => x.Country).IsRequired();
           

        }
    }
}