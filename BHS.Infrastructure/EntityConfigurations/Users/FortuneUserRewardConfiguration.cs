using BHS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Users;

public class FortuneUserRewardConfiguration : IEntityTypeConfiguration<FortuneUserReward>
{
    public void Configure(EntityTypeBuilder<FortuneUserReward> builder)
    {
        builder.ToTable("FortuneUserReward");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.FortuneDetail).WithMany(x => x.FortuneUserRewards).HasForeignKey(x => x.FortuneDetailId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}