using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedFinancePlanTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDistributionPlanItem_ExpenseItem",
                table: "FinanceDistributionPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDistributionPlan_IncomeSource",
                table: "FinanceDistributionPlans");

            migrationBuilder.CreateTable(
                name: "FinanceDistributionPlanTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlannedBudget = table.Column<decimal>(type: "money", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    IncomeSourceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceDistributionPlanTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanTemplate_IncomeSource",
                        column: x => x.IncomeSourceId,
                        principalTable: "IncomeSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanTemplate_User",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinanceDistributionPlanTemplateItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FinanceDistributionPlanTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    PlannedValue = table.Column<decimal>(type: "money", nullable: false),
                    ExpenseItemId = table.Column<long>(type: "bigint", nullable: false),
                    ValueTypeCode = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceDistributionPlanTemplateItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanTemplateItem_ExpenseItem",
                        column: x => x.ExpenseItemId,
                        principalTable: "ExpenseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanTemplateItem_FinanceDistributionPlanTemplate",
                        column: x => x.FinanceDistributionPlanTemplateId,
                        principalTable: "FinanceDistributionPlanTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinanceDistributionPlanTemplateItem_ValueType",
                        column: x => x.ValueTypeCode,
                        principalTable: "FinancesDistributionItemValueTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanTemplateItems_ExpenseItemId",
                table: "FinanceDistributionPlanTemplateItems",
                column: "ExpenseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanTemplateItems_FinanceDistributionPla~",
                table: "FinanceDistributionPlanTemplateItems",
                column: "FinanceDistributionPlanTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanTemplateItems_ValueTypeCode",
                table: "FinanceDistributionPlanTemplateItems",
                column: "ValueTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanTemplates_IncomeSourceId",
                table: "FinanceDistributionPlanTemplates",
                column: "IncomeSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDistributionPlanTemplates_OwnerId",
                table: "FinanceDistributionPlanTemplates",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDistributionPlanItem_ExpenseItem",
                table: "FinanceDistributionPlanItems",
                column: "ExpenseItemId",
                principalTable: "ExpenseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDistributionPlan_IncomeSource",
                table: "FinanceDistributionPlans",
                column: "IncomeSourceId",
                principalTable: "IncomeSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDistributionPlanItem_ExpenseItem",
                table: "FinanceDistributionPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDistributionPlan_IncomeSource",
                table: "FinanceDistributionPlans");

            migrationBuilder.DropTable(
                name: "FinanceDistributionPlanTemplateItems");

            migrationBuilder.DropTable(
                name: "FinanceDistributionPlanTemplates");

            migrationBuilder.DropIndex(
                name: "UX_Users_Email",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDistributionPlanItem_ExpenseItem",
                table: "FinanceDistributionPlanItems",
                column: "ExpenseItemId",
                principalTable: "ExpenseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDistributionPlan_IncomeSource",
                table: "FinanceDistributionPlans",
                column: "IncomeSourceId",
                principalTable: "IncomeSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
