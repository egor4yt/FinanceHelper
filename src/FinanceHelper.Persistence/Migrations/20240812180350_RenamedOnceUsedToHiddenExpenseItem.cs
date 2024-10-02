using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedOnceUsedToHiddenExpenseItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTimeUsable",
                table: "ExpenseItems");

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "ExpenseItems",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Indicates that the expense item created while user creating a finance distribution plan or other automated way");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "ExpenseItems");

            migrationBuilder.AddColumn<bool>(
                name: "OneTimeUsable",
                table: "ExpenseItems",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Indicates that the expense item is using in one step of the finance distribution plan, no more");
        }
    }
}
