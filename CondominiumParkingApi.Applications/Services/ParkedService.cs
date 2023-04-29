using AutoMapper;
using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Exceptions;
using CondominiumParkingApi.Domain.Interfaces;

namespace CondominiumParkingApi.Applications.Services
{
    public class ParkedService : IParkedService
    {
        private readonly IParkedRepository _parkedRepository;
        private readonly IApartmentVehicleRepository _apartmentVehicleRepository;
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        private readonly IMapper _mapper;
        private readonly IParkingSpaceService _parkingSpaceService;
        public ParkedService(IParkedRepository parkedRepository, 
            IApartmentVehicleRepository apartmentVehicleRepository,
            IParkingSpaceRepository parkingSpaceRepository,
            IMapper mapper,
            IParkingSpaceService parkingSpaceService)
        {
            _parkedRepository = parkedRepository;
            _apartmentVehicleRepository = apartmentVehicleRepository;
            _parkingSpaceRepository = parkingSpaceRepository;
            _mapper = mapper;
            _parkingSpaceService = parkingSpaceService;
        }

        public async Task<List<ParkedViewModel>> GetAll(bool active)
        {
            try
            {
                List<Parked> parkeds = new();

                if (active)
                    parkeds = await _parkedRepository.GetAllParkedActive();
                else
                    parkeds = await _parkedRepository.GetAllAsync();

                var results = new List<ParkedViewModel>();

                results.AddRange(parkeds.Select(parked => _mapper.Map<ParkedViewModel>(parked)));

                return results;
            }
            catch
            {
                throw new Exception("ERR-PS001 Falha interna no servidor");
            }
        }

        public async Task<ParkedViewModel> Park(ParkedInputModel entering)
        {
            try
            {
                var vehicle = await _apartmentVehicleRepository.GetActiveLinkByVehicleIdWithInclusions(entering.VehicleId);

                if (vehicle is null)
                    throw new NotFoundException($"ERR-PS002 O veículo solicitado não foi encontrado!");

                var parkingSpaces = await _parkingSpaceService.GetAllParkingSpaces();

                if (parkingSpaces is null)
                    throw new NotFoundException($"ERR-PS002 Não foi encontrada nenhuma vaga no sistema!");

                var space = parkingSpaces.FirstOrDefault(parkingSpace => parkingSpace.Id == entering.ParkingSpaceId);

                if (space is null)
                    throw new NotFoundException($"ERR-PS002 A vaga solicitada não foi encontrada!");
                else if (!space.Free)
                    throw new BadRequestException($"ERR-PS002 A vaga solicitada está em uso!");
                else if (parkingSpaces.FirstOrDefault(a => a.Plate == vehicle.Vehicle.Plate) is not null)
                    throw new BadRequestException($"ERR-PS002 O veículo solicitado ja está estacionado em outra vaga!");

                var parked = new Parked();

                parked.ParkingSpaceId = entering.ParkingSpaceId;
                parked.ApartmentVehicle = vehicle;

                parked.Park();

                await _parkedRepository.InsertAsync(parked);

                return _mapper.Map<ParkedViewModel>(parked);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch
            {
                throw new Exception("ERR-PS004 Falha interna no servidor");
            }
        }

        public async Task<ParkedViewModel> Unpark(decimal parkedId)
        {
            try
            {
                var parked = await _parkedRepository.GetByIdAsync(parkedId);

                if (parked is null)
                    throw new NotFoundException($"ERR-PS005 A atividade solicitada não foi encontrada!");

                if (parked.Out_Date.HasValue || !parked.Active)
                    throw new BadRequestException($"ERR-PS006 A atividade solicitada já está concluída!");

                parked.Unpark();

                await _parkedRepository.UpdateAsync(parked);

                return _mapper.Map<ParkedViewModel>(parked);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch
            {
                throw new Exception("ERR-PS007 Falha interna no servidor");
            }
        }
    }
}