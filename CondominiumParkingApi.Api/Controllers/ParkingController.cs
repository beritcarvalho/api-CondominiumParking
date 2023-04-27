using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
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
                    return NotFound(new ResultViewModel<List<ParkedViewModel>>("ERR-PC001 Nenhum Registro encontrado!"));

                return Ok(new ResultViewModel<List<ParkedViewModel>>(parkingSpaces));
            }
            catch (NotFoundException exception)
            {
                return NotFound(new ResultViewModel<List<ParkedViewModel>>(exception.Message));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<List<ParkedViewModel>>(exception.Message));
            }
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetAllParkedActive()
        {
            try
            {
                var parkingSpaces = await _parkedService.GetAll(true);

                if (parkingSpaces.Count is 0)
                    return NotFound(new ResultViewModel<List<ParkedViewModel>>("ERR-PC002 Nenhum Registro encontrado!"));

                return Ok(new ResultViewModel<List<ParkedViewModel>>(parkingSpaces));
            }
            catch (NotFoundException exception)
            {
                return NotFound(new ResultViewModel<List<ParkedViewModel>>(exception.Message));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<List<ParkedViewModel>>(exception.Message));
            }
        }

        [HttpPost("in")]
        public async Task<IActionResult> Park([FromBody] ParkedInputModel entering)
        {
            try
            {
                var parked = await _parkedService.Park(entering);

                return Ok(new ResultViewModel<ParkedViewModel>(parked));
            }
            catch (NotFoundException exception)
            {
                return NotFound(new ResultViewModel<ParkedViewModel>(exception.Message));
            }
            catch (BadRequestException exception)
            {
                return BadRequest(new ResultViewModel<ParkedViewModel>(exception.Message));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<ParkedViewModel>(exception.Message));
            }
        }

        [HttpPut("out/{parkedId}")]
        public async Task<IActionResult> Unpark([FromRoute] decimal parkedId)
        {
            try
            {
                var parked = await _parkedService.Unpark(parkedId);

                return Ok(new ResultViewModel<ParkedViewModel>(parked));
            }
            catch (NotFoundException exception)
            {
                return NotFound(new ResultViewModel<ParkedViewModel>(exception.Message));
            }
            catch (BadRequestException exception)
            {
                return BadRequest(new ResultViewModel<ParkedViewModel>(exception.Message));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ResultViewModel<ParkedViewModel>(exception.Message));
            }
        }
    }
}