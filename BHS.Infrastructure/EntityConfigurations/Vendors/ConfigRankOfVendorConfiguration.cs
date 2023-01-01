using BHS.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHS.Infrastructure.EntityConfigurations.Vendors;

public class ConfigRankOfVendorConfiguration : IEntityTypeConfiguration<ConfigRankOfVendor>
{
    public void Configure(EntityTypeBuilder<ConfigRankOfVendor> builder)
    {
        builder.ToTable("ConfigRankOfVendor");
        builder.HasKey(x => x.Id);
    }
}