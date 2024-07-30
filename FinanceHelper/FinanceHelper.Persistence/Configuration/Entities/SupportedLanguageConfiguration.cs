using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class SupportedLanguageConfiguration : IEntityTypeConfiguration<SupportedLanguage>
{
    public void Configure(EntityTypeBuilder<SupportedLanguage> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.Code)
            .HasColumnType("varchar(2)")
            .IsRequired();

        builder.Property(x => x.LocalizedValue)
            .HasColumnType("varchar(32)")
            .IsRequired();

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<SupportedLanguage> builder)
    {
        builder.HasData(Domain.Metadata.SupportedLanguage.Russian);
        builder.HasData(Domain.Metadata.SupportedLanguage.English);
    }
}