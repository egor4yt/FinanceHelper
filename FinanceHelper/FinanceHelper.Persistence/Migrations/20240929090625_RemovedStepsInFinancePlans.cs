using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedStepsInFinancePlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepNumber",
                table: "FinanceDistributionPlanItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StepNumber",
                table: "FinanceDistributionPlanItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
