using BHS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Users;

public class PointOfUserConfiguration : IEntityTypeConfiguration<PointOfUser>
{
    public void Configure(EntityTypeBuilder<PointOfUser> builder)
    {
        builder.ToTable("PointOfUser");
        builder.HasKey(x => x.Id);
    }
}