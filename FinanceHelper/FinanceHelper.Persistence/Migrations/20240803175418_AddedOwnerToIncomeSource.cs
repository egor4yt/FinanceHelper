using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedOwnerToIncomeSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "IncomeSources",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeSources_OwnerId",
                table: "IncomeSources",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeSource_User",
                table: "IncomeSources",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeSource_User",
                table: "IncomeSources");

            migrationBuilder.DropIndex(
                name: "IX_IncomeSources_OwnerId",
                table: "IncomeSources");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "IncomeSources");
        }
    }
}
