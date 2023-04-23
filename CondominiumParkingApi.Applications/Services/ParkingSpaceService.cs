using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;

namespace CondominiumParkingApi.Applications.Services
{
    public class ParkingSpaceService : IParkingSpaceService
    {
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        private readonly IParkedRepository _parkedRepository;
        public ParkingSpaceService(IParkingSpaceRepository parkingSpaceRepository,
            IParkedRepository parkedRepository) 
        {
            _parkingSpaceRepository = parkingSpaceRepository;
            _parkedRepository = parkedRepository;
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

        public async Task<List<ParkingSpaceViewModel>> GetAllParkingSpaces()
        {
            var parkingSpaces = await _parkingSpaceRepository.GetAllAsync();
            var parkedActives = await _parkedRepository.GetAllParkedActive();

            var listReturn = new List<ParkingSpaceViewModel>();

            foreach (var item in parkingSpaces)
            {
                var parked = parkedActives.Where(x => x.ParkingSpaceId == item.Id).FirstOrDefault();

                var parkingSpaceReturn = new ParkingSpaceViewModel
                {
                    Id = item.Id,
                    Space = item.Space,
                    Handicap = item.Handicap
                };

                if (parked is not null)
                {
                    parkingSpaceReturn.Free = false;
                    parkingSpaceReturn.Plate = parked.ApartmentVehicle.Vehicle.Plate;
                    parkingSpaceReturn.Apartment = string.Format($"{parked.ApartmentVehicle.Apartment.Number}-{parked.ApartmentVehicle.Apartment.Block.Block_Name}");
                    parkingSpaceReturn.In_Date = parked.In_Date;
                    parkingSpaceReturn.Deadline = parked.Deadline;
                }
                listReturn.Add(parkingSpaceReturn);
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

            foreach (var item in spaces)
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