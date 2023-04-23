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

        public async Task<List<ParkingSpaceViewModel>> CreateNewParkingSpaces(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepararLista(range, true);

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

        public async Task<List<ParkingSpaceViewModel>> EnableByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepararLista(range);
            
            foreach (var item in spaces)
            {
                item.Active = true;
            }

            await _parkingSpaceRepository.UpdateAsync(spaces);

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

        public async Task<List<ParkingSpaceViewModel>> DisableByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepararLista(range);

            foreach (var item in spaces)
            {
                item.Active = false;
            }
            await _parkingSpaceRepository.UpdateAsync(spaces);

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

        public async Task<List<ParkingSpaceViewModel>> DisableHandcapByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepararLista(range);

            foreach (var item in spaces)
            {
                item.Handicap = false;
            }
            await _parkingSpaceRepository.UpdateAsync(spaces);

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

        public async Task<List<ParkingSpaceViewModel>> EnableHandcapByRange(ParkingSpaceInputModel range)
        {
            if (range.From < 1 || range.To < range.From)
                return new List<ParkingSpaceViewModel>();

            List<ParkingSpace> spaces = await PrepararLista(range);

            foreach (var item in spaces)
            {
                item.Handicap = true;
            }
            await _parkingSpaceRepository.UpdateAsync(spaces);

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

        private async Task<List<ParkingSpace>> PrepararLista(ParkingSpaceInputModel range, bool creation = false)
        {
            var spaces = new List<ParkingSpace>();
            spaces = await _parkingSpaceRepository.GetAllAsync();

            var ids = spaces.Select(x => x.Space).ToArray();
            int menor;
            int maior;
            if (ids.Length > 0)
            {
                menor = ids.Min();
                maior = ids.Max();
            }
            else
            {
                menor = range.From;
                maior = range.To;
            }


            for (int i = range.From - 1; i >= menor; i--)
            {
                var space = spaces.FirstOrDefault(x => x.Space == i);

                if (space is not null)
                    spaces.Remove(space);
            }

            for (int i = range.To + 1; i <= maior; i++)
            {
                var space = spaces.FirstOrDefault(x => x.Space == i);

                if (space is not null)
                    spaces.Remove(space);
            }

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

            return spaces;
        }
    }
}