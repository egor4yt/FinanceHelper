using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyIndivisibleToFinancePlanItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Indivisible",
                table: "FinanceDistributionPlanItems",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Indicates that fact value must be calculated without any divides and strict equals to the planned value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Indivisible",
                table: "FinanceDistributionPlanItems");
        }
    }
}
