using CoreReactApp.Server.API;
using CoreReactApp.Server.Repositories;
using CoreReactApp.Server.Services.Implementation;
using CoreReactApp.Server.Services.Interface;

namespace CoreReactApp.Server.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFlightService> _flightService;
        private readonly Lazy<IAirportService> _airportService;

        public IFlightService FlightService => _flightService.Value;

        public IAirportService AirportService => _airportService.Value;

        public ServiceManager(IRepositoryManager repositoryManager, FlightAPI flightAPI)
        {
            _flightService = new Lazy<IFlightService>(() => new FlightService(repositoryManager, flightAPI));
            _airportService = new Lazy<IAirportService>(() => new AirportService(repositoryManager));
        }
    }
}
