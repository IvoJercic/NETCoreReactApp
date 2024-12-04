using CoreReactApp.Server.Services.Interface;

namespace CoreReactApp.Server.Services
{
    public interface IServiceManager
    {
        IFlightService FlightService { get; }

        IAirportService AirportService { get; }
    }
}
