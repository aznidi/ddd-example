using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Infrastructure.Modules.Finance.Persistence.Configurations;

public sealed class BillableServiceConfiguration : IEntityTypeConfiguration<BillableService>
{
    public void Configure(EntityTypeBuilder<BillableService> builder)
    {
        builder.ToTable("billable_services");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new ServiceId(value));

        builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.OwnsOne(x => x.Price, money =>
        {
            money.Property(p => p.Amount).HasColumnName("price_amount").IsRequired();
            money.Property(p => p.Currency).HasColumnName("price_currency").HasMaxLength(5).IsRequired();
        });
    }
}