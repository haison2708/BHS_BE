using BHS.Domain.Entities.Notify;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Notify;

public class NotificationSetUpConfiguration : IEntityTypeConfiguration<NotificationSetUp>
{
    public void Configure(EntityTypeBuilder<NotificationSetUp> builder)
    {
        builder.ToTable("NotificationSetup");
        builder.HasKey(x => x.Id);
    }
}