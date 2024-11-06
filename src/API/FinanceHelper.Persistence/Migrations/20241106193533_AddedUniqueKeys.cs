using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IncomeSources_OwnerId",
                table: "IncomeSources");

            migrationBuilder.DropIndex(
                name: "IX_FinanceDistributionPlanTemplates_OwnerId",
                table: "FinanceDistributionPlanTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseItems_OwnerId",
                table: "ExpenseItems");

            migrationBuilder.CreateIndex(
                name: "UX_IncomeSources_OwnerId_Name",
                table: "IncomeSources",
                columns: new[] { "OwnerId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_FinanceDistributionPlanTemplates_OwnerId_Name",
                table: "FinanceDistributionPlanTemplates",
                columns: new[] { "OwnerId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_ExpenseItems_OwnerId_Name",
                table: "ExpenseItems",
                columns: new[] { "OwnerId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_IncomeSources_OwnerId_Name",
                table: "IncomeSources");

            migrationBuilder.DropIndex(
                name: "UX_FinanceDistributionPlanTemplates_OwnerId_Name",
                table: "FinanceDistributionPlanTemplates");

            migrationBuilder.DropIndex(
                name: "UX_ExpenseItems_OwnerId_Name",
                table: "ExpenseItems");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeSources_OwnerId",
                table: "IncomeSources",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanTemplates_OwnerId",
                table: "FinanceDistributionPlanTemplates",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_OwnerId",
                table: "ExpenseItems",
                column: "OwnerId");
        }
    }
}
