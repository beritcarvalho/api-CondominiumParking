using CondominiumParkingApi.Api.Extensions;
using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
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
                    return NotFound(new ResultViewModel<List<ParkingSpaceViewModel>>("ERR-PC001 Nenhum Registro encontrado!"));

                return Ok(new ResultViewModel<List<ParkingSpaceViewModel>>(parkingSpaces));
            }
            catch (NotFoundException exception)
            {
                return NotFound(new ResultViewModel<List<ParkingSpaceViewModel>>(exception.Message));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<List<ParkingSpaceViewModel>>(exception.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInQuantities([FromBody] RangeInputModel range)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<ParkingSpaceViewModel>(ModelState.GetErrors()));                

                var parkingSpaces = await _parkingSpaceService.CreateNewParkingSpaces(range);

                if (parkingSpaces.Count is 0)
                    return NotFound(new ResultViewModel<List<ParkingSpaceViewModel>>("ERR-PC003 Nenhum Registro encontrado!"));

                return Ok(new ResultViewModel<List<ParkingSpaceViewModel>>(parkingSpaces));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<List<ParkingSpaceViewModel>>(exception.Message));
            }
        }

        [HttpPut("availability/change")]
        public async Task<IActionResult> ChangeParkingSpaceAvailability([FromBody] ChangeParkingSpaceAvailability input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<ParkingSpaceViewModel>(ModelState.GetErrors()));

                var parkingSpaces = await _parkingSpaceService.ChangeParkingSpaceAvailability(input);

                if (parkingSpaces.Count is 0)
                    return NotFound(new ResultViewModel<List<ParkingSpaceViewModel>>("ERR-PC005 Nenhum Registro encontrado!"));

                return Ok(new ResultViewModel<List<ParkingSpaceViewModel>>(parkingSpaces));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<List<ParkingSpaceViewModel>>(exception.Message));
            }
        }

        [HttpPut("handicap/change")]
        public async Task<IActionResult> ChangeReservationOfHandicapped([FromBody] HandicapParkingSpaceInputModel input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<ParkingSpaceViewModel>(ModelState.GetErrors()));               

                var parkingSpaces = await _parkingSpaceService.ChangeReservationOfHandicapped(input);

                if (parkingSpaces.Count is 0)
                    return NotFound(new ResultViewModel<List<ParkingSpaceViewModel>>("ERR-PC007 Nenhum Registro encontrado!"));

                return Ok(parkingSpaces);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<List<ParkingSpaceViewModel>>(exception.Message));
            }
        }
    }
}