using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BHS.Infrastructure;

public class BHSDbContext : DbContext
{
    public BHSDbContext(DbContextOptions<BHSDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}