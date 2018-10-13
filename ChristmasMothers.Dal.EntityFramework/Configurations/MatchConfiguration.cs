using System;
using System.Collections.Generic;
using System.Text;
using ChristmasMothers.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class MatchConfiguration : EntityGuidConfiguration<Match>
    {
        public override void Configure(EntityTypeBuilder<Match> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => new {x.ChildId, x.XMasMotherId});


        }
    }
}
