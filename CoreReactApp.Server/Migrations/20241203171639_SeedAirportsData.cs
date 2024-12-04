using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreReactApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedAirportsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "IATACode", "Name", "Location" },
                values: new object[,]
                {
            { 61, "ATL", "Hartsfield-Jackson Atlanta International Airport", "Atlanta, USA" },
            { 62, "PEK", "Beijing Capital International Airport", "Beijing, China" },
            { 63, "LAX", "Los Angeles International Airport", "Los Angeles, USA" },
            { 64, "DXB", "Dubai International Airport", "Dubai, UAE" },
            { 65, "HND", "Tokyo Haneda Airport", "Tokyo, Japan" },
            { 66, "ORD", "O'Hare International Airport", "Chicago, USA" },
            { 67, "LHR", "London Heathrow Airport", "London, UK" },
            { 68, "CDG", "Charles de Gaulle Airport", "Paris, France" },
            { 69, "DFW", "Dallas/Fort Worth International Airport", "Dallas, USA" },
            { 70, "DEN", "Denver International Airport", "Denver, USA" },
            { 71, "SIN", "Singapore Changi Airport", "Singapore, Singapore" },
            { 72, "AMS", "Amsterdam Schiphol Airport", "Amsterdam, Netherlands" },
            { 73, "ICN", "Incheon International Airport", "Seoul, South Korea" },
            { 74, "BKK", "Suvarnabhumi Airport", "Bangkok, Thailand" },
            { 75, "HKG", "Hong Kong International Airport", "Hong Kong, China" },
            { 76, "JFK", "John F. Kennedy International Airport", "New York, USA" },
            { 77, "KUL", "Kuala Lumpur International Airport", "Kuala Lumpur, Malaysia" },
            { 78, "FRA", "Frankfurt Airport", "Frankfurt, Germany" },
            { 79, "IST", "Istanbul Airport", "Istanbul, Turkey" },
            { 80, "GRU", "São Paulo/Guarulhos–Governador André Franco Montoro International Airport", "São Paulo, Brazil" },
            { 81, "MIA", "Miami International Airport", "Miami, USA" },
            { 82, "SYD", "Sydney Kingsford-Smith Airport", "Sydney, Australia" },
            { 83, "YVR", "Vancouver International Airport", "Vancouver, Canada" },
            { 84, "YYZ", "Toronto Pearson International Airport", "Toronto, Canada" },
            { 85, "MUC", "Munich Airport", "Munich, Germany" },
            { 86, "SFO", "San Francisco International Airport", "San Francisco, USA" },
            { 87, "SEA", "Seattle-Tacoma International Airport", "Seattle, USA" },
            { 88, "LAS", "McCarran International Airport", "Las Vegas, USA" },
            { 89, "PHX", "Phoenix Sky Harbor International Airport", "Phoenix, USA" },
            { 90, "MCO", "Orlando International Airport", "Orlando, USA" },
            { 91, "IAH", "George Bush Intercontinental Airport", "Houston, USA" },
            { 92, "BOS", "Logan International Airport", "Boston, USA" },
            { 93, "MAD", "Adolfo Suárez Madrid–Barajas Airport", "Madrid, Spain" },
            { 94, "BCN", "Barcelona–El Prat Airport", "Barcelona, Spain" },
            { 95, "GIG", "Rio de Janeiro–Galeão International Airport", "Rio de Janeiro, Brazil" },
            { 96, "DEL", "Indira Gandhi International Airport", "Delhi, India" },
            { 97, "BOM", "Chhatrapati Shivaji Maharaj International Airport", "Mumbai, India" },
            { 98, "JNB", "O. R. Tambo International Airport", "Johannesburg, South Africa" },
            { 99, "CPT", "Cape Town International Airport", "Cape Town, South Africa" },
            { 100, "CAI", "Cairo International Airport", "Cairo, Egypt" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int id = 61; id <= 100; id++)
            {
                migrationBuilder.DeleteData(
                    table: "Airports",
                    keyColumn: "AirportId",
                    keyValue: id);
            }
        }
    }
}
