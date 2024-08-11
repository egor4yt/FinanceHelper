using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseItemColorAndTypeNowNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_ExpenseItemType",
                table: "ExpenseItems");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseItemTypeCode",
                table: "ExpenseItems",
                type: "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ExpenseItems",
                type: "varchar(7)",
                nullable: true,
                comment: "HEX-format color",
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldComment: "HEX-format color");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_ExpenseItemType",
                table: "ExpenseItems",
                column: "ExpenseItemTypeCode",
                principalTable: "ExpenseItemTypes",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_ExpenseItemType",
                table: "ExpenseItems");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseItemTypeCode",
                table: "ExpenseItems",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ExpenseItems",
                type: "varchar(7)",
                nullable: false,
                defaultValue: "",
                comment: "HEX-format color",
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldNullable: true,
                oldComment: "HEX-format color");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_ExpenseItemType",
                table: "ExpenseItems",
                column: "ExpenseItemTypeCode",
                principalTable: "ExpenseItemTypes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
