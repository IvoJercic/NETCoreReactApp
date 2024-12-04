using CoreReactApp.Server.Data.DTOs;
using System.Text;
using System.Text.Json;

namespace CoreReactApp.Server.API
{
    public class FlightAPI
    {
        private string apiKey;
        private string apiSecret;
        private string bearerToken;
        private HttpClient http;

        public FlightAPI(IConfiguration config, IHttpClientFactory httpFactory)
        {
            apiKey = config.GetValue<string>("AmadeusAPI:APIKey");
            apiSecret = config.GetValue<string>("AmadeusAPI:APISecret");
            http = httpFactory.CreateClient("FlightAPI");
        }

        public async Task ConnectOAuth()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/v1/security/oauth2/token");
            message.Content = new StringContent(
                $"grant_type=client_credentials&client_id={apiKey}&client_secret={apiSecret}",
                Encoding.UTF8, "application/x-www-form-urlencoded"
            );

            var results = await http.SendAsync(message);
            await using var stream = await results.Content.ReadAsStreamAsync();
            var oauthResults = await JsonSerializer.DeserializeAsync<OAuthResults>(stream);

            bearerToken = oauthResults.access_token;
        }

        private class OAuthResults
        {
            public string access_token { get; set; }
        }

        public async Task<IEnumerable<FlightDTO>> GetFlightsByFilterAsync(FlightFilter flightFilter)
        {
            string endpoint = "https://test.api.amadeus.com/v2/shopping/flight-offers";

            var queryString = BuildQueryString(flightFilter);
            var url = $"{endpoint}?{queryString}";

            var message = new HttpRequestMessage(HttpMethod.Get,
                url);

            ConfigBearerTokenHeader();

            var response = await http.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error retrieving flight data.");
            }

            using var stream = await response.Content.ReadAsStreamAsync();

            var json = await new StreamReader(stream).ReadToEndAsync();

            bool oneWay = !flightFilter.EndDate.HasValue;
            return ParseFlightOffers(json, oneWay);
        }

        private void ConfigBearerTokenHeader()
        {
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
        }

        private static TimeSpan CalculateFlightDuration(string departureTime, string arrivalTime)
        {
            var departureDateTime = DateTime.Parse(departureTime);
            var arrivalDateTime = DateTime.Parse(arrivalTime);
            return arrivalDateTime - departureDateTime;
        }
        private static IEnumerable<FlightDTO> ParseFlightOffers(string json, bool oneWay)
        {
            var root = JsonDocument.Parse(json).RootElement;

            var flights = root.GetProperty("data").EnumerateArray().Select(flight =>
            {
                JsonElement firstItinerary = flight.GetProperty("itineraries")[0];
                JsonElement? lastItinerary = flight.GetProperty("itineraries").EnumerateArray().LastOrDefault();

                if (!lastItinerary.HasValue || lastItinerary.Value.ValueKind == JsonValueKind.Undefined)
                {
                    lastItinerary = firstItinerary;
                }

                if (lastItinerary.HasValue && lastItinerary.Value.Equals(firstItinerary))
                {
                    lastItinerary = null;
                }

                var startSourceIATA = firstItinerary.GetProperty("segments")[0]
                    .GetProperty("departure").GetProperty("iataCode").GetString();

                var startDestinationIATA = firstItinerary.GetProperty("segments")
                    .EnumerateArray().Last()
                    .GetProperty("arrival").GetProperty("iataCode").GetString();

                var startDate = ParseDateTime(firstItinerary.GetProperty("segments")[0]
                    .GetProperty("departure").GetProperty("at").GetString());

                var startArrivalDate = ParseDateTime(firstItinerary.GetProperty("segments").EnumerateArray().Last()
                    .GetProperty("arrival").GetProperty("at").GetString());

                var startNumberOfStops = GetNumberOfStops(firstItinerary);

                var endSourceIATA = lastItinerary?.GetProperty("segments")[0]
                    .GetProperty("departure").GetProperty("iataCode").GetString();

                var endDestinationIATA = lastItinerary?.GetProperty("segments").EnumerateArray().Last()
                    .GetProperty("arrival").GetProperty("iataCode").GetString();

                var endDate = oneWay ? null : ParseDateTime(lastItinerary?.GetProperty("segments")[0]
                    .GetProperty("departure").GetProperty("at").GetString());

                var endArrivalDate = ParseDateTime(lastItinerary?.GetProperty("segments").EnumerateArray().Last()
                    .GetProperty("arrival").GetProperty("at").GetString());

                var endNumberOfStops = GetNumberOfStops(lastItinerary);

                var bookableSeats = flight.GetProperty("numberOfBookableSeats").GetInt32();
                var currency = flight.GetProperty("price").GetProperty("currency").GetString();
                var price = double.Parse(flight.GetProperty("price").GetProperty("grandTotal").GetString()!);

                var startFlightDuration = CalculateFlightDuration(
                    firstItinerary.GetProperty("segments")[0].GetProperty("departure").GetProperty("at").GetString(),
                    firstItinerary.GetProperty("segments").EnumerateArray().Last().GetProperty("arrival").GetProperty("at").GetString()
                );

                var endFlightDuration = oneWay ? (TimeSpan?)null : CalculateFlightDuration(
                    lastItinerary?.GetProperty("segments")[0].GetProperty("departure").GetProperty("at").GetString(),
                    lastItinerary?.GetProperty("segments").EnumerateArray().Last().GetProperty("arrival").GetProperty("at").GetString()
                );

                return new FlightDTO
                {
                    Id = int.Parse(flight.GetProperty("id").GetString()!),
                    StartSourceIATA = startSourceIATA,
                    StartDestinationIATA = startDestinationIATA,
                    StartDate = startDate,
                    StartArrivalDate = startArrivalDate,
                    StartNumberOfStops = startNumberOfStops,
                    EndSourceIATA = endSourceIATA,
                    EndDestinationIATA = endDestinationIATA,
                    EndDate = endDate,
                    EndArrivalDate = endArrivalDate,
                    EndNumberOfStops = endNumberOfStops,
                    BookableSeats = bookableSeats,
                    Currency = currency,
                    Price = price,
                    StartFlightDuration = startFlightDuration,
                    EndFlightDuration = endFlightDuration
                };
            });

            return flights;
        }

        private static DateTime? ParseDateTime(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return null;

            return DateTime.TryParse(dateString, out var parsedDate) ? parsedDate : null;
        }

        private static int GetNumberOfStops(JsonElement? itinerary)
        {
            if (itinerary == null || itinerary.Value.ValueKind != JsonValueKind.Object)
                return 0;

            if (itinerary.Value.TryGetProperty("segments", out var segments) && segments.ValueKind == JsonValueKind.Array)
            {
                var segmentCount = segments.GetArrayLength();
                return Math.Max(0, segmentCount - 1);
            }

            return 0;
        }

        private string BuildQueryString(FlightFilter filter)
        {
            var queryParameters = new List<string>();

            if (!string.IsNullOrEmpty(filter.SourceIATA))
                queryParameters.Add($"originLocationCode={Uri.EscapeDataString(filter.SourceIATA)}");

            if (!string.IsNullOrEmpty(filter.DestinationIATA))
                queryParameters.Add($"destinationLocationCode={Uri.EscapeDataString(filter.DestinationIATA)}");

            if (filter.StartDate.HasValue)
                queryParameters.Add($"departureDate={filter.StartDate.Value:yyyy-MM-dd}");

            if (filter.EndDate.HasValue)
                queryParameters.Add($"returnDate={filter.EndDate.Value:yyyy-MM-dd}");

            if (filter.Passengers.HasValue)
                queryParameters.Add($"adults={filter.Passengers}");

            if (!string.IsNullOrEmpty(filter.Currency))
                queryParameters.Add($"currencyCode={Uri.EscapeDataString(filter.Currency)}");

            return string.Join("&", queryParameters);
        }
    }
}