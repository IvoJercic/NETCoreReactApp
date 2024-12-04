using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreReactApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AirportIATACodeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IATACode",
                table: "Airports",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_IATACode",
                table: "Airports",
                column: "IATACode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Airports_IATACode",
                table: "Airports");

            migrationBuilder.AlterColumn<string>(
                name: "IATACode",
                table: "Airports",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
