using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedAuthorToOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "FinanceDistributionPlans",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_FinanceDistributionPlans_AuthorId",
                table: "FinanceDistributionPlans",
                newName: "IX_FinanceDistributionPlans_OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "FinanceDistributionPlans",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_FinanceDistributionPlans_OwnerId",
                table: "FinanceDistributionPlans",
                newName: "IX_FinanceDistributionPlans_AuthorId");
        }
    }
}
