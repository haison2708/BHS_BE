using BHS.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Products;

public class PromotionalProductConfiguration : IEntityTypeConfiguration<PromotionalProduct>
{
    public void Configure(EntityTypeBuilder<PromotionalProduct> builder)
    {
        builder.ToTable("PromotionalProduct");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Product).WithMany(x => x.PromotionalProduct).HasForeignKey(x => x.ProductId);
        builder.Property(x => x.AmountPromo).HasColumnType("decimal(18,2)").HasDefaultValue(0);
    }
}