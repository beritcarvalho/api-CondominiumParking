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

                return new ParkedViewModel
                {
                    Id = parked.Id,
                    ParkingSpaceId = parked.ParkingSpaceId,
                    ApartmentVehicleId = parked.ApartmentVehicleId,
                    In_Date = parked.In_Date,
                    Out_Date = parked.Out_Date,
                    Active = parked.Active
                };
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

                return new ParkedViewModel
                {
                    Id = parked.Id,
                    ParkingSpaceId = parked.ParkingSpaceId,
                    ApartmentVehicleId = parked.ApartmentVehicleId,
                    In_Date = parked.In_Date,
                    Out_Date = parked.Out_Date,
                    Active = parked.Active,
                    //Exceeded = parked.Total_Exceeded_Minutes.HasValue,
                    //Time_Exceeded = parked.Total_Exceeded_Minutes.HasValue ? TimeSpan.FromMinutes((double)parked.Total_Exceeded_Minutes) : null
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}