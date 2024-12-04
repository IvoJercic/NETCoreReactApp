using CoreReactApp.Server.Entities;

namespace CoreReactApp.Server.Services.Interface
{
    public interface IAirportService
    {
        Task<bool> CreateAirport(Airport flight);

        Task<IEnumerable<Airport>> GetAllAirports();

        Task<Airport?> GetAirportById(int flightId);

        Task<bool> UpdateAirport(Airport flight);

        Task<bool> DeleteAirport(int flightId);
    }
}
