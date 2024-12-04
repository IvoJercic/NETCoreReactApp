using CoreReactApp.Server.API;
using CoreReactApp.Server.Data.DTOs;
using CoreReactApp.Server.Entities;
using CoreReactApp.Server.Mappers;
using CoreReactApp.Server.Repositories;
using CoreReactApp.Server.Services.Interface;

namespace CoreReactApp.Server.Services.Implementation
{
    public class FlightService : IFlightService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly FlightAPI _flightAPI;

        public FlightService(IRepositoryManager repositoryManager, FlightAPI flightAPI)
        {
            _repositoryManager = repositoryManager;
            _flightAPI = flightAPI;
        }

        public async Task<bool> CreateFlight(FlightDTO model)
        {
            if (model != null)
            {
                Airport source = await _repositoryManager.Airports.GetByIATACodeAsync(model?.StartSourceIATA);
                Airport destination = await _repositoryManager.Airports.GetByIATACodeAsync(model?.StartDestinationIATA);

                Flight flight = model.MapToEntity(source, destination);
                await _repositoryManager.Flights.Add(flight);

                int result = _repositoryManager.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteFlight(int flightId)
        {
            if (flightId > 0)
            {
                Flight flight = await _repositoryManager.Flights.GetById(flightId);
                if (flight != null)
                {
                    _repositoryManager.Flights.Delete(flight);
                    int result= _repositoryManager.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Flight>> GetAllFlights()
        {
            IEnumerable<Flight> flightList = await _repositoryManager.Flights.GetAll();
            return flightList;
        }

        public async Task<Flight?> GetFlightById(int flightId)
        {
            if (flightId > 0)
            {
                Flight flight = await _repositoryManager.Flights.GetById(flightId);
                if (flight != null)
                {
                    return flight;
                }
            }
            return null;
        }

        public async Task<bool> UpdateFlight(Flight model)
        {
            if (model != null)
            {
                Flight flight = await _repositoryManager.Flights.GetById(model.FlightId);
                if (flight != null)
                {
                    flight.Currency = model.Currency;

                    _repositoryManager.Flights.Update(flight);

                    int result = _repositoryManager.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<FlightDTO>> GetFlightsByFliter(FlightFilter flightFilter)
        {
            await _flightAPI.ConnectOAuth();
            IEnumerable<FlightDTO> results = await _flightAPI.GetFlightsByFilterAsync(flightFilter);
            return (results);
        }

        public async Task<IEnumerable<FlightDTO>> GetFavorites()
        {
            var favoriteFlights = await _repositoryManager.Flights.GetFavoritesAsync();

            IEnumerable<FlightDTO> results = favoriteFlights.Select(flight => flight.MapToDTO());
            return results;
        }
    }
}
