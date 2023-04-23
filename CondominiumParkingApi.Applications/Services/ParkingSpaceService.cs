using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using System;

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
            var parkingSpaces = await _parkingSpaceRepository.GetAllAsync();

            var results = new List<ParkingSpaceViewModel>();

            foreach (var space in parkingSpaces)
            {
                results.Add(new ParkingSpaceViewModel
                {
                    Id = space.Id,
                    Space = space.Space,
                    Handicap = space.Handicap
                });
            }

            return results;
        }

        public async Task<List<ParkingSpaceViewModel>> GetAllParkingSpaces()
        {
            var parkingSpaces = await _parkingSpaceRepository.GetAllAsync();
            var parkedActives = await _parkedRepository.GetAllParkedActive();

            var results = new List<ParkingSpaceViewModel>();

            foreach (var space in parkingSpaces)
            {
                var parked = parkedActives.Where(x => x.ParkingSpaceId == space.Id).FirstOrDefault();

                var result = new ParkingSpaceViewModel
                {
                    Id = space.Id,
                    Space = space.Space,
                    Handicap = space.Handicap
                };

                if (parked is not null)
                {
                    result.Free = false;
                    result.Plate = parked.ApartmentVehicle.Vehicle.Plate;
                    result.Apartment = string.Format($"{parked.ApartmentVehicle.Apartment.Number}-{parked.ApartmentVehicle.Apartment.Block.Block_Name}");
                    result.In_Date = parked.In_Date;
                    result.Deadline = parked.Deadline;
                }
                results.Add(result);
            }

            return results;
        }

        public async Task<List<ParkingSpaceViewModel>> CreateNewParkingSpaces(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepareList(range, true);

            await _parkingSpaceRepository.InsertRangeAsync(spaces);

            var results = new List<ParkingSpaceViewModel>();

            foreach (var space in spaces)
            {
                results.Add(new ParkingSpaceViewModel
                {
                    Id = space.Id,
                    Space = space.Space,
                    Handicap = space.Handicap
                });
            }

            return results;
        }

        public async Task<List<ParkingSpaceViewModel>> EnableByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepareList(range);

            spaces.ForEach(space => space.EnableParkingSpace());

            await _parkingSpaceRepository.UpdateAsync(spaces);

            var results = new List<ParkingSpaceViewModel>();

            results.AddRange(spaces.Select(space =>
                new ParkingSpaceViewModel
                {
                    Id = space.Id,
                    Space = space.Space,
                    Handicap = space.Handicap
                }));

            return results;
        }

        public async Task<List<ParkingSpaceViewModel>> DisableByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepareList(range);

            foreach (var space in spaces)
            {
                space.DisableParkingSpace();
            }
            await _parkingSpaceRepository.UpdateAsync(spaces);

            var results = new List<ParkingSpaceViewModel>();

            foreach (var item in spaces)
            {
                results.Add(new ParkingSpaceViewModel
                {
                    Id = item.Id,
                    Space = item.Space,
                    Handicap = item.Handicap
                });
            }

            return results;
        }

        public async Task<List<ParkingSpaceViewModel>> DisableHandcapByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepareList(range);

            foreach (var space in spaces)
            {
                space.DisableHandicap();
            }
            await _parkingSpaceRepository.UpdateAsync(spaces);

            var results = new List<ParkingSpaceViewModel>();

            foreach (var space in spaces)
            {
                results.Add(new ParkingSpaceViewModel
                {
                    Id = space.Id,
                    Space = space.Space,
                    Handicap = space.Handicap
                });
            }

            return results;
        }

        public async Task<List<ParkingSpaceViewModel>> EnableHandcapByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepareList(range);

            foreach (var space in spaces)
            {
                space.EnableHandicap();
            }
            await _parkingSpaceRepository.UpdateAsync(spaces);

            var results = new List<ParkingSpaceViewModel>();

            foreach (var space in spaces)
            {
                results.Add(new ParkingSpaceViewModel
                {
                    Id = space.Id,
                    Space = space.Space,
                    Handicap = space.Handicap
                });
            }

            return results;
        }

        private async Task<List<ParkingSpace>> PrepareList(ParkingSpaceInputModel range, bool creation = false)
        {
            var spaces = new List<ParkingSpace>();
            spaces = await _parkingSpaceRepository.GetAllAsync();

            var numbers = spaces.Select(x => x.Space).ToArray();
            int min;
            int max;
            if (numbers.Length > 0)
            {
                min = numbers.Min();
                max = numbers.Max();
            }
            else
            {
                min = range.From;
                max = range.To;
            }

            //removes all parking spaces less than "from"
            for (int i = range.From - 1; i >= min; i--)
            {
                var space = spaces.FirstOrDefault(x => x.Space == i);

                if (space is not null)
                    spaces.Remove(space);
            }

            //removes all parking spaces greater than "to"
            for (int i = range.To + 1; i <= max; i++)
            {
                var space = spaces.FirstOrDefault(x => x.Space == i);

                if (space is not null)
                    spaces.Remove(space);
            }

            //removes all existing parking spaces or adds new spaces between "from" to "to"
            if (creation)
            {
                for (int i = range.From; i <= range.To; i++)
                {
                    var space = spaces.FirstOrDefault(x => x.Space == i);
                    if (space is not null)
                        spaces.Remove(space);
                    else
                        spaces.Add(new ParkingSpace
                        {
                            Space = i
                        });
                }
            }

            return spaces.OrderBy(x => x.Space).ToList();
        }
    }
}