using BHS.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Categories;

public class CategoryOfVendorConfiguration : IEntityTypeConfiguration<CategoryOfVendor>
{
    public void Configure(EntityTypeBuilder<CategoryOfVendor> builder)
    {
        builder.ToTable("CategoryOfVendor");
        builder.HasKey(x => x.Id);
    }
}