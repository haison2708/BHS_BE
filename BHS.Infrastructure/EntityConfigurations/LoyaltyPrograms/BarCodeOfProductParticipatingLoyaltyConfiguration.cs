using BHS.Domain.Entities.LoyaltyPrograms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.LoyaltyPrograms;

public class
    BarCodeOfProductParticipatingLoyaltyConfiguration : IEntityTypeConfiguration<BarCodeOfProductParticipatingLoyalty>
{
    public void Configure(EntityTypeBuilder<BarCodeOfProductParticipatingLoyalty> builder)
    {
        builder.ToTable("BarCodeOfProductParticipatingLoyalty");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BarCode).IsRequired();
        builder.HasIndex(x => new { x.BarCode });
    }
}