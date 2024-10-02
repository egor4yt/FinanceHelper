using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OneTimeUsableExpenseItemsDefaultValueRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OneTimeUsable",
                table: "ExpenseItems",
                type: "boolean",
                nullable: false,
                comment: "Indicates that the expense item is using in one step of the finance distribution plan, no more",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false,
                oldComment: "Expense item used in one step of the finance distribution plan, no more");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OneTimeUsable",
                table: "ExpenseItems",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Expense item used in one step of the finance distribution plan, no more",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldComment: "Indicates that the expense item is using in one step of the finance distribution plan, no more");
        }
    }
}
