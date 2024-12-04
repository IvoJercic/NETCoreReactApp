using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreReactApp.Server.Entities
{
    public class Flight
    {
        /// <summary>
        /// FlightId
        /// </summary>        
        [Key]
        public int FlightId { get; set; }

        /// <summary>
        /// Polazni aerodrom
        /// </summary>
        public required int SourceId { get; set; }
        [ForeignKey("SourceId")]
        public required Airport Source { get; set; }

        /// <summary>
        /// Odredišni aerodrom
        /// </summary>
        public required int DestinationId { get; set; }
        [ForeignKey("DestinationId")]
        public required Airport Destination { get; set; }        

        /// <summary>
        /// Datum polaska sa polazista
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Datum dolaska na odredište
        /// </summary>
        public required DateTime StartArrivalDate { get; set; }

        /// <summary>
        /// Datum polaska sa "odredišta"
        /// </summary>
        public required DateTime? EndDate { get; set; }

        /// <summary>
        /// Datum dolaska na "polazište"
        /// </summary>
        public required DateTime? EndArrivalDate { get; set; }

        /// <summary>
        /// Broj presjedanja na odlasku
        /// </summary>
        public required int? StartNumberOfStops { get; set; }


        /// <summary>
        /// Datum presjedanja na povratku
        /// </summary>
        public required int? EndNumberOfStops { get; set; }

        /// <summary>
        /// Broj slobodnih mjesta
        /// </summary>
        public int? BookableSeats { get; set; }

        /// <summary>
        /// Valuta
        /// </summary>
        public required string Currency { get; set; }

        /// <summary>
        /// Ukupna cijena
        /// </summary>
        public required string Price { get; set; }

        /// <summary>
        /// Jednosmjerni ili ne
        /// </summary>
        public required bool IsOneWay { get; set; } = false;
    }
}
