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
        public ParkedService(IParkedRepository parkedRepository, 
            IApartmentVehicleRepository apartmentVehicleRepository,
            IParkingSpaceRepository parkingSpaceRepository,
            IMapper mapper)
        {
            _parkedRepository = parkedRepository;
            _apartmentVehicleRepository = apartmentVehicleRepository;
            _parkingSpaceRepository = parkingSpaceRepository;
            _mapper = mapper;
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

                if (parkeds.Count == 0)
                    throw new NotFoundException($"ERR-PS001 Nenhum Registro encontrado!");

                var results = new List<ParkedViewModel>();

                results.AddRange(parkeds.Select(parked => _mapper.Map<ParkedViewModel>(parked)));

                return results;
            }
            catch(NotFoundException)
            {
                throw;
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
                var vehicle = await _apartmentVehicleRepository.GetByIdAsync(entering.ApartmentVehicleId);

                if (vehicle is null)
                    throw new NotFoundException($"ERR-PS002 O veículo solicitado não foi encontrado!");

                var busySpace = await _parkedRepository.GetInUseByParkingSpaceId(entering.ParkingSpaceId);

                if (busySpace is not null)
                    throw new BadRequestException($"ERR-PS003 A vaga {busySpace.ParkingSpace.Space} já está em uso!");

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