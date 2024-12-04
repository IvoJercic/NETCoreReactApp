using CoreReactApp.Server.Repositories.Interface;
using CoreReactApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using CoreReactApp.Server.Data;

namespace CoreReactApp.Server.Repositories.Implementation
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Flight>> GetFavoritesAsync()
        {
            var query = _dbContext.Flights.Include(f => f.Source).Include(f => f.Destination).AsQueryable();

            return await query.ToListAsync(); 
        }
    }
}
