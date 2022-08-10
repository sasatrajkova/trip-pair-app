using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tripPairAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateLocationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoodMonthsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

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
    }
}
