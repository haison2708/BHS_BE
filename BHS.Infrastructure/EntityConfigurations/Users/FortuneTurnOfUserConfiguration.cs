using BHS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Users;

public class FortuneTurnOfUserConfiguration : IEntityTypeConfiguration<FortuneTurnOfUser>
{
    public void Configure(EntityTypeBuilder<FortuneTurnOfUser> builder)
    {
        builder.ToTable("FortuneTurnOfUser");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TurnAvailable).IsConcurrencyToken();
    }
}