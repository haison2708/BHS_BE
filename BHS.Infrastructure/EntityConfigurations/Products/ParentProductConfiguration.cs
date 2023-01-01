using BHS.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Products;

public class ParentProductConfiguration : IEntityTypeConfiguration<ParentProduct>
{
    public void Configure(EntityTypeBuilder<ParentProduct> builder)
    {
        builder.ToTable("ParentProduct");
        builder.HasKey(p => p.Id);
    }
}