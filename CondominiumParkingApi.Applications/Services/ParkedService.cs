using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using System.Runtime.Intrinsics.X86;

namespace CondominiumParkingApi.Applications.Services
{
    public class ParkedService : IParkedService
    {
        private readonly IParkedRepository _parkedRepository;
        private readonly IApartmentVehicleRepository _apartmentVehicleRepository;
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        public ParkedService(IParkedRepository parkedRepository, 
            IApartmentVehicleRepository apartmentVehicleRepository,
            IParkingSpaceRepository parkingSpaceRepository)
        {
            _parkedRepository = parkedRepository;
            _apartmentVehicleRepository = apartmentVehicleRepository;
            _parkingSpaceRepository = parkingSpaceRepository;
        }

        public async Task<List<ParkedViewModel>> GetAll()
        {
            var activeParked = await _parkedRepository.GetAllAsync();

            var listReturn = new List<ParkedViewModel>();

            foreach (var item in activeParked)
            {
                listReturn.Add(new ParkedViewModel
                {
                    Id = item.Id,
                    ParkingSpaceId = item.ParkingSpaceId,
                    ApartmentVehicleId = item.ApartmentVehicleId,
                    In_Date = item.In_Date,
                    Out_Date = item.Out_Date,
                    Active = item.Active
                });
            }

            return listReturn;
        }

        public async Task<List<ParkedViewModel>> GetAllParkedActive()
        {
            var activeParked = await _parkedRepository.GetAllParkedActive();

            var listReturn = new List<ParkedViewModel>();
            
            foreach (var item in activeParked)
            {
                listReturn.Add(new ParkedViewModel
                {
                    Id = item.Id,
                    ParkingSpaceId = item.ParkingSpaceId,
                    ApartmentVehicleId = item.ApartmentVehicleId,
                    In_Date = item.In_Date,
                    Out_Date = item.Out_Date,
                    Active = item.Active
                });
            }

            return listReturn;
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
                    Exceeded = parked.Total_Exceeded_Minutes.HasValue,
                    Time_Exceeded = parked.Total_Exceeded_Minutes.HasValue ? TimeSpan.FromMinutes((double)parked.Total_Exceeded_Minutes) : null
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}