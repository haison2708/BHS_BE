using BHS.Domain.Entities.Fortunes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Fortunes;

public class FortuneConfiguration : IEntityTypeConfiguration<Fortune>
{
    public void Configure(EntityTypeBuilder<Fortune> builder)
    {
        builder.ToTable("Fortune");
        builder.HasKey(x => x.Id);
    }
}