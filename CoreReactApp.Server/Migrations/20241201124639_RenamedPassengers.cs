using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreReactApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPassengers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passengers",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "BookableSeats",
                table: "Flights",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookableSeats",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "Passengers",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
