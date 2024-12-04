using CoreReactApp.Server.Data.DTOs;
using CoreReactApp.Server.Entities;

namespace CoreReactApp.Server.Mappers
{
    public static class FlightMapper
    {
        public static FlightDTO MapToDTO(this Flight flight)
        {
            return new FlightDTO
            {
                Id = flight.FlightId,
                StartSourceIATA = flight.Source?.IATACode, 
                StartDestinationIATA = flight.Destination?.IATACode,
                EndSourceIATA = flight.Destination?.IATACode,
                EndDestinationIATA = flight.Source?.IATACode,
                StartDate = flight.StartDate,
                StartArrivalDate = flight.StartArrivalDate,
                EndDate = flight.EndDate,
                EndArrivalDate = flight.EndArrivalDate,
                StartNumberOfStops = flight.StartNumberOfStops,
                EndNumberOfStops = flight.EndNumberOfStops,                
                BookableSeats = flight.BookableSeats,
                Currency = flight.Currency,
                Price = double.TryParse(flight.Price, out var price) ? price : (double?)null, 
                StartFlightDuration = flight.StartDate != null && flight.StartArrivalDate != null
                    ? flight.StartArrivalDate - flight.StartDate
                    : (TimeSpan?)null,
                EndFlightDuration = flight.EndDate != null && flight.EndArrivalDate != null
                    ? flight.EndArrivalDate - flight.EndDate
                    : (TimeSpan?)null
            };
        }

        public static Flight MapToEntity(this FlightDTO flightDTO,Airport source, Airport destination)
        {
            return new Flight
            {
                Source = source,                
                Destination  = destination,
                SourceId = source.AirportId,
                DestinationId = destination.AirportId,
                StartDate = flightDTO.StartDate ?? DateTime.MinValue, 
                StartArrivalDate = flightDTO.StartArrivalDate ?? DateTime.MinValue, 
                EndDate = flightDTO.EndDate,
                EndArrivalDate = flightDTO.EndArrivalDate,
                StartNumberOfStops = flightDTO.StartNumberOfStops,
                EndNumberOfStops = flightDTO.EndNumberOfStops,
                BookableSeats = flightDTO.BookableSeats,
                Currency = flightDTO.Currency ?? string.Empty, 
                Price = flightDTO.Price.HasValue ? flightDTO.Price.ToString() : "0", 
                IsOneWay = flightDTO.EndSourceIATA == null || flightDTO.EndDestinationIATA == null 
            };
        }
    }
}
