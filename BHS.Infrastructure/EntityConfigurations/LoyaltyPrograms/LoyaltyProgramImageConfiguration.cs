using BHS.Domain.Entities.LoyaltyPrograms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.LoyaltyPrograms;

public class LoyaltyProgramImageConfiguration : IEntityTypeConfiguration<LoyaltyProgramImage>
{
    public void Configure(EntityTypeBuilder<LoyaltyProgramImage> builder)
    {
        builder.ToTable("LoyaltyProgramImage");
        builder.HasKey(x => x.Id);
    }
}