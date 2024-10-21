using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class IncomeSourceConfiguration : IEntityTypeConfiguration<IncomeSource>
{
    public void Configure(EntityTypeBuilder<IncomeSource> builder)
    {
        builder
            .HasOne(x => x.IncomeSourceType)
            .WithMany(x => x.IncomeSources)
            .HasForeignKey(x => x.IncomeSourceTypeCode)
            .HasConstraintName("FK_IncomeSource_IncomeSourceType");

        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.IncomeSources)
            .HasForeignKey(x => x.OwnerId)
            .HasConstraintName("FK_IncomeSource_User");

        builder
            .Property(x => x.Color)
            .IsRequired()
            .HasComment("HEX-format color")
            .HasColumnType("varchar(7)");

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.DeletedAt)
            .HasColumnType("timestamptz");

        builder
            .Property(x => x.IncomeSourceTypeCode)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.OwnerId)
            .IsRequired();
    }
}