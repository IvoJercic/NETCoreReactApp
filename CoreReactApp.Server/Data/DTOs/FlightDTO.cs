namespace CoreReactApp.Server.Data.DTOs
{
    public class FlightDTO
    {
        public int Id { get; set; }

        public string? StartSourceIATA { get; set; }
        public string? StartSourceLocation { get; set; }
        public string? StartDestinationIATA { get; set; }
        public string? StartDestinationLocation { get; set; }
        public string? EndSourceIATA { get; set; }
        public string? EndDestinationIATA { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? StartArrivalDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EndArrivalDate { get; set; }

        public int? BookableSeats { get; set; }
        public string? Currency { get; set; }
        public double? Price { get; set; }

        public int? StartNumberOfStops { get; set; }
        public int? EndNumberOfStops { get; set; }

        public TimeSpan? StartFlightDuration { get; set; }
        public TimeSpan? EndFlightDuration { get; set; }
    }
}
