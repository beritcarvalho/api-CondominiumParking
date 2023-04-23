using AutoMapper;
using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
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
            List<Parked> activeParkeds = new();

            if (active)
                activeParkeds = await _parkedRepository.GetAllParkedActive();
            else
                activeParkeds = await _parkedRepository.GetAllAsync();

            var results = new List<ParkedViewModel>();  

            results.AddRange(activeParkeds.Select(parked => _mapper.Map<ParkedViewModel>(parked)));

            return results;
        }

        public async Task<ParkedViewModel> Park(ParkedInputModel entering)
        {
            var vehicle = await _apartmentVehicleRepository.GetByIdAsync(entering.ApartmentVehicleId);
            var busySpace = await _parkedRepository.GetInUseByParkingSpaceId(entering.ParkingSpaceId) is not null;

            if (busySpace)
                return null;

            var parked = new Parked();

            parked.ParkingSpaceId = entering.ParkingSpaceId;
            parked.ApartmentVehicle = vehicle;
            parked.Park();

            try
            {
                await _parkedRepository.InsertAsync(parked);

                return _mapper.Map<ParkedViewModel>(parked);

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ParkedViewModel> Unpark(ParkedInputModel leaving)
        {
            try
            {
                var parked = await _parkedRepository.GetByIdAsync(leaving.ParkedId);

                if (parked is null)
                    return null;
            
                parked.Unpark();

                await _parkedRepository.UpdateAsync(parked);

                return _mapper.Map<ParkedViewModel>(parked);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}