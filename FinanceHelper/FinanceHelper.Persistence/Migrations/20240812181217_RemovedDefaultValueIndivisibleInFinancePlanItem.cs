using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDefaultValueIndivisibleInFinancePlanItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Indivisible",
                table: "FinanceDistributionPlanItems",
                type: "boolean",
                nullable: false,
                comment: "Indicates that fact value must be calculated without any divides and strict equals to the planned value",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false,
                oldComment: "Indicates that fact value must be calculated without any divides and strict equals to the planned value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Indivisible",
                table: "FinanceDistributionPlanItems",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Indicates that fact value must be calculated without any divides and strict equals to the planned value",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldComment: "Indicates that fact value must be calculated without any divides and strict equals to the planned value");
        }
    }
}
