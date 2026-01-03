using Harmonix.Shared.Data.DbConfig;
using Harmonix.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonix.Shared.Data;

public class HarmonixDbContext : DbContext
{
    public HarmonixDbContext(DbContextOptions<HarmonixDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserDbConfig());
        modelBuilder.ApplyConfiguration(new CompanyDbConfig());

        base.OnModelCreating(modelBuilder);
    }
}
