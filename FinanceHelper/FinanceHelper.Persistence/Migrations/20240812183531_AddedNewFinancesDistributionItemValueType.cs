using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFinancesDistributionItemValueType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FinancesDistributionItemValueTypes",
                columns: new[] { "Code", "LocalizationKeyword" },
                values: new object[] { "fixed-indivisible", "FixedIndivisible" });

            migrationBuilder.UpdateData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "Fixed", "finances-distribution-item-value-type", "en" },
                column: "LocalizedValue",
                value: "Fixed divisible");

            migrationBuilder.UpdateData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "Fixed", "finances-distribution-item-value-type", "ru" },
                column: "LocalizedValue",
                value: "Фиксированный делимый");

            migrationBuilder.InsertData(
                table: "MetadataLocalizations",
                columns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode", "LocalizedValue" },
                values: new object[,]
                {
                    { "FixedIndivisible", "finances-distribution-item-value-type", "en", "Fixed indivisible" },
                    { "FixedIndivisible", "finances-distribution-item-value-type", "ru", "Фиксированный неделимый" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FinancesDistributionItemValueTypes",
                keyColumn: "Code",
                keyValue: "fixed-indivisible");

            migrationBuilder.DeleteData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "FixedIndivisible", "finances-distribution-item-value-type", "en" });

            migrationBuilder.DeleteData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "FixedIndivisible", "finances-distribution-item-value-type", "ru" });

            migrationBuilder.UpdateData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "Fixed", "finances-distribution-item-value-type", "en" },
                column: "LocalizedValue",
                value: "Fixed");

            migrationBuilder.UpdateData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "Fixed", "finances-distribution-item-value-type", "ru" },
                column: "LocalizedValue",
                value: "Фиксированный");
        }
    }
}
