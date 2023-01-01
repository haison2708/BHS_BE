using BHS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Users;

public class UserAppTokenConfiguration : IEntityTypeConfiguration<UserAppToken>
{
    public void Configure(EntityTypeBuilder<UserAppToken> builder)
    {
        builder.ToTable("UserAppToken");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithMany(x => x.UserAppToken).HasForeignKey(x => x.UserId);
        builder.Property(x => x.AppId).IsRequired();
        builder.Property(x => x.Token).IsRequired();
    }
}