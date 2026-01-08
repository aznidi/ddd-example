using Microsoft.EntityFrameworkCore;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.Entities;

namespace SMS.Infrastructure.Persistence;

public sealed class SmsDbContext : DbContext
{
    public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<BillableService> BillableServices => Set<BillableService>();
    public DbSet<Engagement> Engagements => Set<Engagement>();
    public DbSet<EngagementLine> EngagementLines => Set<EngagementLine>();
    public DbSet<Tranche> Tranches => Set<Tranche>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmsDbContext).Assembly);
    }
}   