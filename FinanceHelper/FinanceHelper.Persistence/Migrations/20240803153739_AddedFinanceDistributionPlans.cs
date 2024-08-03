using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedFinanceDistributionPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinanceDistributionPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlannedBudget = table.Column<decimal>(type: "money", nullable: false),
                    FactBudget = table.Column<decimal>(type: "money", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceDistributionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlan_Author",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinanceDistributionPlanItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinanceDistributionPlanId = table.Column<long>(type: "bigint", nullable: false),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    FactValue = table.Column<decimal>(type: "money", nullable: false),
                    PlannedValue = table.Column<decimal>(type: "money", nullable: false),
                    ExpenseItemId = table.Column<long>(type: "bigint", nullable: false),
                    ValueTypeCode = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceDistributionPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanItem_ExpenseItem",
                        column: x => x.ExpenseItemId,
                        principalTable: "ExpenseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanItem_FinanceDistributionPlan",
                        column: x => x.FinanceDistributionPlanId,
                        principalTable: "FinanceDistributionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanItem_ValueType",
                        column: x => x.ValueTypeCode,
                        principalTable: "FinancesDistributionItemValueTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanItems_ExpenseItemId",
                table: "FinanceDistributionPlanItems",
                column: "ExpenseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanItems_FinanceDistributionPlanId",
                table: "FinanceDistributionPlanItems",
                column: "FinanceDistributionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanItems_ValueTypeCode",
                table: "FinanceDistributionPlanItems",
                column: "ValueTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlans_AuthorId",
                table: "FinanceDistributionPlans",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinanceDistributionPlanItems");

            migrationBuilder.DropTable(
                name: "FinanceDistributionPlans");
        }
    }
}
