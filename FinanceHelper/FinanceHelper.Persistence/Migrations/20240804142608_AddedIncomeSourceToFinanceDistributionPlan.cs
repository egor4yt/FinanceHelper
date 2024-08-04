using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIncomeSourceToFinanceDistributionPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDistributionPlan_Author",
                table: "FinanceDistributionPlans");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "FinanceDistributionPlans",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<long>(
                name: "IncomeSourceId",
                table: "FinanceDistributionPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlans_IncomeSourceId",
                table: "FinanceDistributionPlans",
                column: "IncomeSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDistributionPlan_IncomeSource",
                table: "FinanceDistributionPlans",
                column: "IncomeSourceId",
                principalTable: "IncomeSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDistributionPlan_User",
                table: "FinanceDistributionPlans",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDistributionPlan_IncomeSource",
                table: "FinanceDistributionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDistributionPlan_User",
                table: "FinanceDistributionPlans");

            migrationBuilder.DropIndex(
                name: "IX_FinanceDistributionPlans_IncomeSourceId",
                table: "FinanceDistributionPlans");

            migrationBuilder.DropColumn(
                name: "IncomeSourceId",
                table: "FinanceDistributionPlans");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "FinanceDistributionPlans",
                newName: "CreationDate");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDistributionPlan_Author",
                table: "FinanceDistributionPlans",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
