using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMetadataAndSeedGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseItemTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(32)", nullable: false),
                    LocalizationKeyword = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItemTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "FinancesDistributionItemValueTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(32)", nullable: false),
                    LocalizationKeyword = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancesDistributionItemValueTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "IncomeSourceTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(32)", nullable: false),
                    LocalizationKeyword = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeSourceTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "MetadataTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "SupportedLanguages",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(2)", nullable: false),
                    LocalizedValue = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedLanguages", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "TagTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "MetadataLocalizations",
                columns: table => new
                {
                    SupportedLanguageCode = table.Column<string>(type: "varchar(2)", nullable: false),
                    LocalizationKeyword = table.Column<string>(type: "varchar(32)", nullable: false),
                    MetadataTypeCode = table.Column<string>(type: "varchar(64)", nullable: false),
                    LocalizedValue = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataLocalizations", x => new { x.SupportedLanguageCode, x.LocalizationKeyword, x.MetadataTypeCode });
                    table.ForeignKey(
                        name: "FK_MetadataLocalization_MetadataType",
                        column: x => x.MetadataTypeCode,
                        principalTable: "MetadataTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataLocalization_SupportedLanguage",
                        column: x => x.SupportedLanguageCode,
                        principalTable: "SupportedLanguages",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpenseItemTypes",
                columns: new[] { "Code", "LocalizationKeyword" },
                values: new object[,]
                {
                    { "charity", "Charity" },
                    { "debt", "Debt" },
                    { "expense", "Expense" },
                    { "investment", "Investment" },
                    { "other", "Other" }
                });

            migrationBuilder.InsertData(
                table: "FinancesDistributionItemValueTypes",
                columns: new[] { "Code", "LocalizationKeyword" },
                values: new object[,]
                {
                    { "fixed", "Fixed" },
                    { "floating", "Floating" }
                });

            migrationBuilder.InsertData(
                table: "IncomeSourceTypes",
                columns: new[] { "Code", "LocalizationKeyword" },
                values: new object[,]
                {
                    { "debt", "Debt" },
                    { "deposit-withdraw", "DepositWithdraw" },
                    { "investment", "Investment" },
                    { "other", "Other" },
                    { "work", "Work" }
                });

            migrationBuilder.InsertData(
                table: "MetadataTypes",
                column: "Code",
                values: new object[]
                {
                    "expense-item-type",
                    "finances-distribution-item-value-type",
                    "income-source-type"
                });

            migrationBuilder.InsertData(
                table: "SupportedLanguages",
                columns: new[] { "Code", "LocalizedValue" },
                values: new object[,]
                {
                    { "en", "English" },
                    { "ru", "Русский" }
                });

            migrationBuilder.InsertData(
                table: "TagTypes",
                column: "Code",
                value: "income-source");

            migrationBuilder.InsertData(
                table: "MetadataLocalizations",
                columns: new[] { "LocalizationKeyword", "MetadataTypeCode", "SupportedLanguageCode", "LocalizedValue" },
                values: new object[,]
                {
                    { "Charity", "expense-item-type", "en", "Charity" },
                    { "Debt", "expense-item-type", "en", "Debt" },
                    { "Debt", "income-source-type", "en", "Debt" },
                    { "DepositWithdraw", "expense-item-type", "en", "Bank deposit" },
                    { "DepositWithdraw", "income-source-type", "en", "Bank deposit withdraw" },
                    { "Expense", "expense-item-type", "en", "Regular expenses" },
                    { "Fixed", "finances-distribution-item-value-type", "en", "Fixed" },
                    { "Floating", "finances-distribution-item-value-type", "en", "Floating" },
                    { "Investment", "expense-item-type", "en", "Investment" },
                    { "Investment", "income-source-type", "en", "Investment" },
                    { "Other", "expense-item-type", "en", "Other" },
                    { "Other", "income-source-type", "en", "Other" },
                    { "Work", "income-source-type", "en", "Work" },
                    { "Charity", "expense-item-type", "ru", "Благотворительность" },
                    { "Debt", "expense-item-type", "ru", "Долг" },
                    { "Debt", "income-source-type", "ru", "Долг" },
                    { "DepositWithdraw", "expense-item-type", "ru", "Вклад" },
                    { "DepositWithdraw", "income-source-type", "ru", "Снятие со вклада" },
                    { "Expense", "expense-item-type", "ru", "Регулярные траты" },
                    { "Fixed", "finances-distribution-item-value-type", "ru", "Фиксированный" },
                    { "Floating", "finances-distribution-item-value-type", "ru", "Процентный" },
                    { "Investment", "expense-item-type", "ru", "Инвестиции" },
                    { "Investment", "income-source-type", "ru", "Инвестиции" },
                    { "Other", "expense-item-type", "ru", "Другое" },
                    { "Other", "income-source-type", "ru", "Другое" },
                    { "Work", "income-source-type", "ru", "Работа" }
                });

            migrationBuilder.CreateIndex(
                name: "UX_Code_LocalizationKeyword",
                table: "FinancesDistributionItemValueTypes",
                columns: new[] { "Code", "LocalizationKeyword" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Code_LocalizationKeyword1",
                table: "IncomeSourceTypes",
                columns: new[] { "Code", "LocalizationKeyword" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetadataLocalizations_MetadataTypeCode",
                table: "MetadataLocalizations",
                column: "MetadataTypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseItemTypes");

            migrationBuilder.DropTable(
                name: "FinancesDistributionItemValueTypes");

            migrationBuilder.DropTable(
                name: "IncomeSourceTypes");

            migrationBuilder.DropTable(
                name: "MetadataLocalizations");

            migrationBuilder.DropTable(
                name: "TagTypes");

            migrationBuilder.DropTable(
                name: "MetadataTypes");

            migrationBuilder.DropTable(
                name: "SupportedLanguages");
        }
    }
}
