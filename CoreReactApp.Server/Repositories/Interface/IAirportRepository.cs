using CoreReactApp.Server.Entities;

namespace CoreReactApp.Server.Repositories.Interface
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<Airport> GetByIATACodeAsync(string iataCode);
    }
}
