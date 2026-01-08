using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Infrastructure.Modules.Finance.Persistence.Configurations;

public sealed class EngagementConfiguration : IEntityTypeConfiguration<Engagement>
{
    public void Configure(EntityTypeBuilder<Engagement> builder)
    {
        builder.ToTable("engagements");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new EngagementId(value));

        builder.Property(x => x.StudentId)
            .HasConversion(id => id.Value, value => new StudentId(value))
            .IsRequired();

        builder.OwnsOne(x => x.TotalAmount, money =>
        {
            money.Property(p => p.Amount).HasColumnName("total_amount").IsRequired();
            money.Property(p => p.Currency).HasColumnName("total_currency").HasMaxLength(5).IsRequired();
        });

        builder.HasMany(e => e.Lines)
            .WithOne()
            .HasForeignKey("engagement_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Tranches)
            .WithOne()
            .HasForeignKey("engagement_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(e => e.Lines).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(e => e.Tranches).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}