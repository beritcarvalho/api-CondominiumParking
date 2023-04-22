using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;

namespace CondominiumParkingApi.Applications.Services
{
    public class ParkedService : IParkedService
    {
        private readonly IParkedRepository _parkedRepository;
        public ParkedService(IParkedRepository parkedRepository)
        {
            _parkedRepository = parkedRepository;
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

        public Task<ParkedViewModel> Park(decimal id)
        {
            return null;
        }
    }
}