using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;

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
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInQuantities([FromBody] RangeInputModel range)
        {
            try
            {
                if (range.From < 1 || range.To < range.From)
                    return NotFound();

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

        [HttpPut("availability/change")]
        public async Task<IActionResult> ChangeParkingSpaceAvailability([FromBody] ChangeParkingSpaceAvailability input)
        {
            try
            {
                if (input.From < 1 || input.To < input.From)
                    return NotFound();

                var parkingSpaces = await _parkingSpaceService.ChangeParkingSpaceAvailability(input);

                if (parkingSpaces.Count is 0)
                    return NotFound();

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("handicap/change")]
        public async Task<IActionResult> ChangeReservationOfHandicapped([FromBody] HandicapParkingSpaceInputModel input)
        {
            try
            {
                if (input.From < 1 || input.To < input.From)
                    return NotFound();

                var parkingSpaces = await _parkingSpaceService.ChangeReservationOfHandicapped(input);

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