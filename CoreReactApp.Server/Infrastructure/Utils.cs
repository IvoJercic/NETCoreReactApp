using CoreReactApp.Server.Data.DTOs;

namespace CoreReactApp.Server.Infrastructure
{
    public static class Utils
    {
        public static string GenerateCacheKey(FlightFilter filterData)
        {
            return $"Flights_{filterData.StartDate}_{filterData.EndDate}_{filterData.SourceIATA}_{filterData.DestinationIATA}_{filterData.Passengers}_{filterData.Currency}";
        }
    }
}
