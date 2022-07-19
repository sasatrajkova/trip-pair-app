using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tripPairAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdjustResorttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Resorts",
                table: "Resorts");

            migrationBuilder.RenameTable(
                name: "Resorts",
                newName: "Resort");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resort",
                table: "Resort",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Resort",
                table: "Resort");

            migrationBuilder.RenameTable(
                name: "Resort",
                newName: "Resorts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resorts",
                table: "Resorts",
                column: "Id");
        }
    }
}
