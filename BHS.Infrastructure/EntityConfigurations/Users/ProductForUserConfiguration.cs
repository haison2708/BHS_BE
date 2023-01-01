using BHS.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Users;

public class ProductForUserConfiguration : IEntityTypeConfiguration<ProductForUser>
{
    public void Configure(EntityTypeBuilder<ProductForUser> builder)
    {
        builder.ToTable("ProductForUser");
        builder.HasKey(x => x.Id);
    }
}