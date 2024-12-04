using CoreReactApp.Server.Repositories.Interface;
using CoreReactApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using CoreReactApp.Server.Data;

namespace CoreReactApp.Server.Repositories.Implementation
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        public AirportRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Airport?> GetByIATACodeAsync(string iataCode)
        {
            var query = await _dbContext.Airports.AsQueryable().FirstOrDefaultAsync(x=> x.IATACode == iataCode);

            return query;
        }
    }
}
