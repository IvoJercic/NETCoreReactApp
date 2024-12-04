using CoreReactApp.Server.Data.DTOs;
using CoreReactApp.Server.Entities;
using CoreReactApp.Server.Infrastructure;

namespace CoreReactApp.Server.Services.Interface
{
    public interface IFlightService
    {
        Task<bool> CreateFlight(FlightDTO flight);

        Task<IEnumerable<Flight>> GetAllFlights();

        Task<Flight?> GetFlightById(int flightId);

        Task<bool> UpdateFlight(Flight flight);

        Task<bool> DeleteFlight(int flightId);

        Task<IEnumerable<FlightDTO>> GetFlightsByFliter(FlightFilter flightFilter);

        Task<IEnumerable<FlightDTO>> GetFavorites();
    }
}
