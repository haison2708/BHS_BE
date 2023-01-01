using BHS.Domain.Entities.LoyaltyPrograms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.LoyaltyPrograms;

public class GiftOfLoyaltyConfiguration : IEntityTypeConfiguration<GiftOfLoyalty>
{
    public void Configure(EntityTypeBuilder<GiftOfLoyalty> builder)
    {
        builder.HasIndex(x => x.QtyAvailable);
        builder.ToTable("GiftOfLoyalty");
        builder.HasKey(x => x.Id);
    }
}