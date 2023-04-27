using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Domain.Exceptions;
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
                var parkingSpaces = await _parkedService.GetAll(false);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetAllParkedActive()
        {
            try
            {
                var parkingSpaces = await _parkedService.GetAll(true);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost("in")]
        public async Task<IActionResult> Park([FromBody] ParkedInputModel entering)
        {
            try
            {
                var parked = await _parkedService.Park(entering);

                return Ok(parked);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("out/{parkedId}")]
        public async Task<IActionResult> Unpark([FromRoute] decimal parkedId)
        {
            try
            {
                var parked = await _parkedService.Unpark(parkedId);

                return Ok(parked);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}