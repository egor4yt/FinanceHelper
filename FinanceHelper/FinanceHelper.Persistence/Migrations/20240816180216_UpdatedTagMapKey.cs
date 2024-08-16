using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTagMapKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagMap",
                table: "TagMap");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TagMap");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagMap",
                table: "TagMap",
                columns: new[] { "TagId", "EntityId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagMap",
                table: "TagMap");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "TagMap",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagMap",
                table: "TagMap",
                column: "Id");
        }
    }
}
