using CondominiumParkingApi.Applications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CondominiumParkingApi.Api.Controllers
{
    [ApiController]
    [Route("v1/parkingspaces")]
    public class ParkingSpaceController : ControllerBase
    {
        private readonly IParkingSpaceService _parkingSpaceService;

        public ParkingSpaceController(IParkingSpaceService parkingSpaceService)
        {
            _parkingSpaceService = parkingSpaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParkingSpaces()
        {
            try
            {
                var parkingSpaces = await _parkingSpaceService.GetAllParkingSpaces();

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int quantity)
        {
            try
            {
                var parkingSpaces = await _parkingSpaceService.CreateNewParkingSpaces(quantity);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}