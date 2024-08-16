using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceHelper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTagsMapTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagMap",
                table: "TagMap");

            migrationBuilder.RenameTable(
                name: "TagMap",
                newName: "TagsMap");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagsMap",
                table: "TagsMap",
                columns: new[] { "TagId", "EntityId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagsMap",
                table: "TagsMap");

            migrationBuilder.RenameTable(
                name: "TagsMap",
                newName: "TagMap");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagMap",
                table: "TagMap",
                columns: new[] { "TagId", "EntityId" });
        }
    }
}
