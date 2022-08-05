using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tripPairAPI.Migrations
{
    /// <inheritdoc />
    public partial class Createtablerelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Resorts");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Resorts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Resorts_LocationId",
                table: "Resorts",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resorts_Locations_LocationId",
                table: "Resorts",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resorts_Locations_LocationId",
                table: "Resorts");

            migrationBuilder.DropIndex(
                name: "IX_Resorts_LocationId",
                table: "Resorts");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Resorts");

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Resorts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
