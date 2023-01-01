using BHS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Users;

public class GiftOfUserConfiguration : IEntityTypeConfiguration<GiftOfUser>
{
    public void Configure(EntityTypeBuilder<GiftOfUser> builder)
    {
        builder.ToTable("GiftOfUser");
        builder.HasKey(x => x.Id);
    }
}