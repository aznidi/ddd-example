using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Infrastructure.Modules.Finance.Persistence.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure ( EntityTypeBuilder<Student> builder )
    {
        builder.ToTable("students");

        builder.HasKey(x => x.Id);

        builder.Property( x => x.Id )
            .HasConversion( id => id.Value, value => new StudentId(value));

        builder.Property( x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

        builder.Property( x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();   

        builder.OwnsOne( x => x.BirthDate, bd =>
        {
            bd.Property( p => p.Value ).HasColumnName("birth_date")
                                        .IsRequired();
        } );
    }
}