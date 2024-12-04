using CoreReactApp.Server.Entities;

namespace CoreReactApp.Server.Repositories.Interface
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Task<IEnumerable<Flight>> GetFavoritesAsync();//Nepromjenjiv za razliku od IList, moze mu se pristupit priko LINQ i foreach a IList moze i priko indexa
    }
}

