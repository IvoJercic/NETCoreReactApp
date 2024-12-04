using Microsoft.AspNetCore.Mvc;
using CoreReactApp.Server.Entities;
using CoreReactApp.Server.Services;

namespace CoreReactApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AirportController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Get the list of Airports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAirportList()
        { 
            var airportList = await _service.AirportService.GetAllAirports();
            if (airportList == null)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Get airport by id
        /// </summary>
        /// <param name="airportId"></param>
        /// <returns></returns>
        [HttpGet("{airportId}")]
        public async Task<IActionResult> GetAirportById(int airportId)
        {
            var airport = await _service.AirportService.GetAirportById(airportId);

            if (airport != null)
            {
                return Ok(airport);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add a new airport
        /// </summary>
        /// <param name="airport"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAirport(Airport airport)
        {
            var isSuccess = await _service.AirportService.CreateAirport(airport);

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
        /// Update the airport
        /// </summary>
        /// <param name="airport"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAirport(Airport airport)
        {
            if (airport != null)
            {
                var isSuccess = await _service.AirportService.UpdateAirport(airport);
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
        /// Delete airport by id
        /// </summary>
        /// <param name="airportId"></param>
        /// <returns></returns>
        [HttpDelete("{airportId}")]
        public async Task<IActionResult> DeleteAirport(int airportId)
        {
            var isSuccess = await _service.AirportService.DeleteAirport(airportId);

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
