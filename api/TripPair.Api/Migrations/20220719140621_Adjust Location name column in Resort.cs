using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tripPairAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdjustLocationnamecolumninResort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Resorts",
                newName: "LocationName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "Resorts",
                newName: "Location");
        }
    }
}
