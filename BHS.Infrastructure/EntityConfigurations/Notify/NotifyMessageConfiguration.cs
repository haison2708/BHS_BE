using BHS.Domain.Entities.Notify;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Notify;

public class NotifyMessageConfiguration : IEntityTypeConfiguration<NotifyMessage>
{
    public void Configure(EntityTypeBuilder<NotifyMessage> builder)
    {
        builder.ToTable("NotifyMessage");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.NotificationSetUp).WithMany(x => x.NotifyMessages)
            .HasForeignKey(x => x.NotificationSetUpId);
    }
}