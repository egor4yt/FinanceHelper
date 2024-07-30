using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class MetadataLocalizationConfiguration : IEntityTypeConfiguration<MetadataLocalization>
{
    public void Configure(EntityTypeBuilder<MetadataLocalization> builder)
    {
        builder.HasKey(x => new { LocalizationCode = x.SupportedLanguageCode, x.LocalizationKeyword, x.MetadataTypeCode });

        builder
            .HasOne(x => x.SupportedLanguage)
            .WithMany(x => x.MetadataLocalizations)
            .HasConstraintName("FK_MetadataLocalization_SupportedLanguage");
        
        builder
            .HasOne(x => x.MetadataType)
            .WithMany(x => x.MetadataLocalizations)
            .HasConstraintName("FK_MetadataLocalization_MetadataType");
        
        builder.Property(x => x.SupportedLanguageCode)
            .HasColumnType("varchar(2)")
            .IsRequired();

        builder.Property(x => x.LocalizationKeyword)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(x => x.LocalizedValue)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(x => x.MetadataTypeCode)
            .HasColumnType("varchar(64)")
            .IsRequired();
        
        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<MetadataLocalization> builder)
    {
        builder.HasData(Domain.Metadata.MetadataLocalization.IncomeSourceTypesRu);
        builder.HasData(Domain.Metadata.MetadataLocalization.IncomeSourceTypesEn);
        builder.HasData(Domain.Metadata.MetadataLocalization.ExpenseItemTypesRu);
        builder.HasData(Domain.Metadata.MetadataLocalization.ExpenseItemTypesEn);
        builder.HasData(Domain.Metadata.MetadataLocalization.FinancesDistributionItemValueTypesRu);
        builder.HasData(Domain.Metadata.MetadataLocalization.FinancesDistributionItemValueTypesEn);
    }
}