using BHS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Vendors;

public class UserFollowVendorConfiguration : IEntityTypeConfiguration<UserFollowVendor>
{
    public void Configure(EntityTypeBuilder<UserFollowVendor> builder)
    {
        builder.ToTable("UserFollowVendor");
        builder.HasKey(x => x.Id);
    }
}