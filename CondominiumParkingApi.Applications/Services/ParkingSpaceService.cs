using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;

namespace CondominiumParkingApi.Applications.Services
{
    public class ParkingSpaceService : IParkingSpaceService
    {
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        public ParkingSpaceService(IParkingSpaceRepository parkingSpaceRepository) 
        {
            _parkingSpaceRepository = parkingSpaceRepository;
        }

        public async Task<List<ParkingSpaceViewModel>> GetAll()
        {
            var activeParked = await _parkingSpaceRepository.GetAllAsync();

            var listReturn = new List<ParkingSpaceViewModel>();

            foreach (var item in activeParked)
            {
                listReturn.Add(new ParkingSpaceViewModel
                {
                    Id = item.Id,
                    Space = item.Space,
                    Handicap = item.Handicap
                });
            }

            return listReturn;
        }

        public async Task<List<ParkingSpaceViewModel>> CreateNewParkingSpaces(int quantity)
        {
            var spaces = new List<ParkingSpace>();

            for (int i = 1; i <= quantity; i++)
            {
                spaces.Add(new ParkingSpace
                {
                    Space = i
                });
            }

            await _parkingSpaceRepository.InsertRangeAsync(spaces);

            var returns = new List<ParkingSpaceViewModel>();

            foreach(var item in spaces)
            {
                returns.Add(new ParkingSpaceViewModel
                {
                    Id = item.Id,
                    Space = item.Space,
                    Handicap = item.Handicap
                });
            }

            return returns;
        }
    }
}