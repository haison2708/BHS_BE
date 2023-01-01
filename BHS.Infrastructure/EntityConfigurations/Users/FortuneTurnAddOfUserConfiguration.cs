using BHS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Users;

public class FortuneTurnAddOfUserConfiguration : IEntityTypeConfiguration<FortuneTurnAddOfUser>
{
    public void Configure(EntityTypeBuilder<FortuneTurnAddOfUser> builder)
    {
        builder.ToTable("FortuneTurnAddOfUser");
        builder.HasKey(x => x.Id);
    }
}