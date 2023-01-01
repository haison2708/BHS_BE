using BHS.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Products;

public class AttributeConfiguration : IEntityTypeConfiguration<Attributes>
{
    public void Configure(EntityTypeBuilder<Attributes> builder)
    {
        builder.ToTable("Attributes");
        builder.HasKey(x => x.Id);
    }
}