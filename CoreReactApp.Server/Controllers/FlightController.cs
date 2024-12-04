using Microsoft.AspNetCore.Mvc;
using CoreReactApp.Server.Entities;
using Microsoft.Extensions.Caching.Memory;
using CoreReactApp.Server.Data.DTOs;
using CoreReactApp.Server.Services;
using CoreReactApp.Server.Infrastructure;

namespace CoreReactApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IMemoryCache _memoryCache;

        public FlightController(IServiceManager service, IMemoryCache memoryCache)
        {
            _service = service;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get the list of Flights from API by flightFilter
        /// </summary>
        /// <param name="flightFilter"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        public async Task<IActionResult> GetFlightsByFliter([FromBody] FlightFilter filterData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string cacheKey = Utils.GenerateCacheKey(filterData);
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<FlightDTO>? result))
            {
                return Ok(result);
            }
            else
            {
                var response = await _service.FlightService.GetFlightsByFliter(filterData);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(120))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(cacheKey, response, cacheOptions);

                if (response != null)
                {
                    return Ok(response);
                }

                return NotFound();
            }
        }

        /// <summary>
        /// Get the list of Flights saved in database
        /// </summary>        
        /// <returns></returns>
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var response = await _service.FlightService.GetFavorites();
            if (response != null)
            {
                return Ok(response);
            }

            return NotFound();
        }

        /// <summary>
        /// Get the list of Flights
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFlightList()
        { 
            var flightList = await _service.FlightService.GetAllFlights();
            if (flightList == null)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Get flight by id
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        [HttpGet("{flightId}")]
        public async Task<IActionResult> GetFlightById(int flightId)
        {
            var flight = await _service.FlightService.GetFlightById(flightId);

            if (flight != null)
            {
                return Ok(flight);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add a new flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        [HttpPost("CreateFlight")]
        public async Task<IActionResult> CreateFlight([FromBody] FlightDTO flight)
        {
            var isSuccess = await _service.FlightService.CreateFlight(flight);

            if (isSuccess)
            {
                return Ok(isSuccess);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update the flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateFlight(Flight flight)
        {
            if (flight != null)
            {
                var isSuccess = await _service.FlightService.UpdateFlight(flight);
                if (isSuccess)
                {
                    return Ok(isSuccess);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete flight by id
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteFlight/{flightId}")]
        public async Task<IActionResult> DeleteFlight(int flightId)
        {
            var isSuccess = await _service.FlightService.DeleteFlight(flightId);

            if (isSuccess)
            {
                return Ok(isSuccess);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
