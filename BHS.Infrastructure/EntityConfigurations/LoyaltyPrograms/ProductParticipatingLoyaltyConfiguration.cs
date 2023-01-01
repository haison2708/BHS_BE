using BHS.Domain.Entities.LoyaltyPrograms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.LoyaltyPrograms;

public class ProductParticipatingLoyaltyConfiguration : IEntityTypeConfiguration<ProductParticipatingLoyalty>
{
    public void Configure(EntityTypeBuilder<ProductParticipatingLoyalty> builder)
    {
        builder.ToTable("ProductParticipatingLoyalty");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Product).WithMany(x => x.ProductParticipatingLoyalties).HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}