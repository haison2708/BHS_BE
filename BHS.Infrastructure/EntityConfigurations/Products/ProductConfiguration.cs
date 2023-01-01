using BHS.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(x => x.Id);
        builder.Property(cb => cb.ProductCode).IsRequired();
        builder.Property(cb => cb.Name).IsRequired();
        builder.Property(cb => cb.Unit).IsRequired();
        builder.Property(cb => cb.Qty).IsRequired().HasDefaultValue(-1);
        builder.HasIndex(p => new { p.Barcode });
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
    }
}