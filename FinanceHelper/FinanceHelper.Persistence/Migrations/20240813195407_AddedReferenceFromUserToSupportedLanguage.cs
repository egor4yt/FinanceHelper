using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedReferenceFromUserToSupportedLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_PreferredLocalizationCode",
                table: "Users",
                column: "PreferredLocalizationCode");

            migrationBuilder.AddForeignKey(
                name: "FK_User_SupportedLocalization",
                table: "Users",
                column: "PreferredLocalizationCode",
                principalTable: "SupportedLanguages",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_SupportedLocalization",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PreferredLocalizationCode",
                table: "Users");
        }
    }
}
