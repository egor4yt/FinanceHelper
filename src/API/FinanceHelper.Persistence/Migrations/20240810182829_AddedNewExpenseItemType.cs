using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewExpenseItemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "DepositWithdraw", "expense-item-type", "en" });

            migrationBuilder.DeleteData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "DepositWithdraw", "expense-item-type", "ru" });

            migrationBuilder.InsertData(
                table: "ExpenseItemTypes",
                columns: new[] { "Code", "LocalizationKeyword" },
                values: new object[] { "deposit", "Deposit" });

            migrationBuilder.InsertData(
                table: "MetadataLocalizations",
                columns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode", "LocalizedValue" },
                values: new object[,]
                {
                    { "Deposit", "expense-item-type", "en", "Bank deposit" },
                    { "Deposit", "expense-item-type", "ru", "Вклад" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExpenseItemTypes",
                keyColumn: "Code",
                keyValue: "deposit");

            migrationBuilder.DeleteData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "Deposit", "expense-item-type", "en" });

            migrationBuilder.DeleteData(
                table: "MetadataLocalizations",
                keyColumns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode" },
                keyValues: new object[] { "Deposit", "expense-item-type", "ru" });

            migrationBuilder.InsertData(
                table: "MetadataLocalizations",
                columns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode", "LocalizedValue" },
                values: new object[,]
                {
                    { "DepositWithdraw", "expense-item-type", "en", "Bank deposit" },
                    { "DepositWithdraw", "expense-item-type", "ru", "Вклад" }
                });
        }
    }
}
