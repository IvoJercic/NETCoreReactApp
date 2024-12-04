using CoreReactApp.Server.Data;
using CoreReactApp.Server.Entities;
using CoreReactApp.Server.Repositories.Implementation;
using CoreReactApp.Server.Repositories.Interface;

namespace CoreReactApp.Server.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _dbContext;

        private readonly Lazy<IFlightRepository> _flightRepository;
        //private readonly Lazy<IRepository<Airport>> _airportRepository;
        private readonly Lazy<IAirportRepository> _airportRepository;

        public IFlightRepository Flights => _flightRepository.Value;

        public IAirportRepository Airports => _airportRepository.Value;

        public RepositoryManager(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;

            _flightRepository = new Lazy<IFlightRepository>(() => new FlightRepository(dbContext));
            //_airportRepository = new Lazy<IRepository<Airport>>(() => new Repository<Airport>(dbContext));
            _airportRepository = new Lazy<IAirportRepository>(() => new AirportRepository(dbContext));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
