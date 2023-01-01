using BHS.Domain.Entities.Fortunes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Fortunes;

public class FortuneDetailConfiguration : IEntityTypeConfiguration<FortuneDetail>
{
    public void Configure(EntityTypeBuilder<FortuneDetail> builder)
    {
        builder.ToTable("FortuneDetail");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.QtyAvailable).IsConcurrencyToken();
    }
}