using System.ComponentModel.DataAnnotations;

namespace CoreReactApp.Server.Entities
{
    public class Airport
    {
        /// <summary>
        /// AirportID
        /// </summary>        
        [Key]
        public required int AirportId { get; set; }

        /// <summary>
        /// IATA �ifra aerodroma
        /// </summary>
        public required string IATACode { get; set; }

        /// <summary>
        /// Naziv aerodroma
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Lokacija aerodroma
        /// </summary>
        public required string Location { get; set; }
    }
}
