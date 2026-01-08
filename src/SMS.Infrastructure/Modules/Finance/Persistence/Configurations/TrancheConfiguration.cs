using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Infrastructure.Modules.Finance.Persistence.Configurations;

public sealed class TrancheConfiguration : IEntityTypeConfiguration<Tranche>
{
    public void Configure(EntityTypeBuilder<Tranche> builder)
    {
        builder.ToTable("tranches");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new TrancheId(value));

        builder.Property(x => x.DueDate).HasColumnName("due_date").IsRequired();

        builder.OwnsOne(x => x.AmountDue, money =>
        {
            money.Property(p => p.Amount).HasColumnName("amount_due").IsRequired();
            money.Property(p => p.Currency).HasColumnName("currency").HasMaxLength(5).IsRequired();
        });

        builder.Property(x => x.Status).HasConversion<string>().HasColumnName("status").HasMaxLength(20).IsRequired();
    }
}