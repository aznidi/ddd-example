using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Infrastructure.Modules.Finance.Persistence.Configurations;

public sealed class EngagementLineConfiguration : IEntityTypeConfiguration<EngagementLine>
{
    public void Configure(EntityTypeBuilder<EngagementLine> builder)
    {
        builder.ToTable("engagement_lines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new EngagementLineId(value));

        builder.Property(x => x.ServiceId)
            .HasConversion(id => id.Value, value => new ServiceId(value))
            .IsRequired();

        builder.Property(x => x.ServiceNameSnapshot).HasMaxLength(150).IsRequired();

        builder.OwnsOne(x => x.PriceSnapshot, money =>
        {
            money.Property(p => p.Amount).HasColumnName("price_amount").IsRequired();
            money.Property(p => p.Currency).HasColumnName("price_currency").HasMaxLength(5).IsRequired();
        });

        builder.OwnsOne(x => x.Quantity, q =>
        {
            q.Property(p => p.Value).HasColumnName("quantity").IsRequired();
        });

        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();

        builder.HasIndex("engagement_id", nameof(EngagementLine.ServiceId)).IsUnique();
    }
}