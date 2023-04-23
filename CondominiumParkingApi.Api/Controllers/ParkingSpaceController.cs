using CondominiumParkingApi.Applications.InputModels;
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
        public async Task<IActionResult> CreateInQuantities([FromBody] ParkingSpaceInputModel range)
        {
            try
            {
                var parkingSpaces = await _parkingSpaceService.CreateNewParkingSpaces(range);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("enable")]
        public async Task<IActionResult> EnableParkingSpacesByRange([FromBody] ParkingSpaceInputModel range)
        {
            try
            {
                var parkingSpaces = await _parkingSpaceService.EnableByRange(range);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("disable")]
        public async Task<IActionResult> DisableParkingSpacesByRange([FromBody] ParkingSpaceInputModel range)
        {
            try
            {
                var parkingSpaces = await _parkingSpaceService.DisableByRange(range);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("handcap/disable")]
        public async Task<IActionResult> DisableHandcapByRange([FromBody] ParkingSpaceInputModel range)
        {
            try
            {
                var parkingSpaces = await _parkingSpaceService.DisableHandcapByRange(range);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("handcap/enable")]
        public async Task<IActionResult> EnableHandcapByRange([FromBody] ParkingSpaceInputModel range)
        {
            try
            {
                var parkingSpaces = await _parkingSpaceService.EnableHandcapByRange(range);

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