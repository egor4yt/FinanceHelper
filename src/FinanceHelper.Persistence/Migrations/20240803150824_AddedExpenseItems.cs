using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpenseItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    ExpenseItemTypeCode = table.Column<string>(type: "varchar(32)", nullable: false),
                    Color = table.Column<string>(type: "varchar(7)", nullable: false, comment: "HEX-format color"),
                    DeletedAt = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseItem_ExpenseItemType",
                        column: x => x.ExpenseItemTypeCode,
                        principalTable: "ExpenseItemTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TagTypes",
                column: "Code",
                value: "expense-item");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_ExpenseItemTypeCode",
                table: "ExpenseItems",
                column: "ExpenseItemTypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseItems");

            migrationBuilder.DeleteData(
                table: "TagTypes",
                keyColumn: "Code",
                keyValue: "expense-item");
        }
    }
}
