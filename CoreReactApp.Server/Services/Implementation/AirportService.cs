using CoreReactApp.Server.Entities;
using CoreReactApp.Server.Repositories;
using CoreReactApp.Server.Services.Interface;

namespace CoreReactApp.Server.Services.Implementation
{
    public class AirportService : IAirportService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AirportService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<bool> CreateAirport(Airport airport)
        {
            if (airport != null)
            {
                await _repositoryManager.Airports.Add(airport);

                int result = _repositoryManager.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteAirport(int airportId)
        {
            if (airportId > 0)
            {
                Airport airport = await _repositoryManager.Airports.GetById(airportId);
                if (airport != null)
                {
                    _repositoryManager.Airports.Delete(airport);
                    int result= _repositoryManager.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Airport>> GetAllAirports()
        {
            IEnumerable<Airport> airportList = await _repositoryManager.Airports.GetAll();
            return airportList;
        }

        public async Task<Airport?> GetAirportById(int airportId)
        {
            if (airportId > 0)
            {
                Airport airport = await _repositoryManager.Airports.GetById(airportId);
                if (airport != null)
                {
                    return airport;
                }
            }
            return null;
        }

        public async Task<bool> UpdateAirport(Airport model)
        {
            if (model != null)
            {
                Airport airport = await _repositoryManager.Airports.GetById(model.AirportId);
                if (airport != null)
                {
                    airport.Name = "model.airport";

                    _repositoryManager.Airports.Update(airport);

                    int result = _repositoryManager.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
