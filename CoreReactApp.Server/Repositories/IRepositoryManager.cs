using CoreReactApp.Server.Entities;
using CoreReactApp.Server.Repositories.Interface;

namespace CoreReactApp.Server.Repositories
{
    public interface IRepositoryManager : IDisposable
    {
        IFlightRepository Flights { get; }

        IAirportRepository Airports { get; }

        int Save();
    }
}
