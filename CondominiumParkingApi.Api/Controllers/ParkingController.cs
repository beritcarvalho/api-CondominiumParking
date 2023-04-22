using CondominiumParkingApi.Applications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CondominiumParkingApi.Api.Controllers
{
    [ApiController]
    [Route("v1/parking")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkedService _parkedService;

        public ParkingController(IParkedService parkedService)
        {
            _parkedService = parkedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var parkingSpaces = await _parkedService.GetAll();

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