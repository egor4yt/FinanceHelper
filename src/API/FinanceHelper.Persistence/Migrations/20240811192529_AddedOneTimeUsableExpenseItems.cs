using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneTimeUsableExpenseItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OneTimeUsable",
                table: "ExpenseItems",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Expense item used in one step of the finance distribution plan, no more");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTimeUsable",
                table: "ExpenseItems");
        }
    }
}
