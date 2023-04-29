using AutoMapper;
using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;
using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Exceptions;
using CondominiumParkingApi.Domain.Interfaces;
using System;

namespace CondominiumParkingApi.Applications.Services
{
    public class ParkingSpaceService : IParkingSpaceService
    {
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        private readonly IParkedRepository _parkedRepository;
        private readonly IMapper _mapper;
        public ParkingSpaceService(IParkingSpaceRepository parkingSpaceRepository,
            IParkedRepository parkedRepository,
            IMapper mapper) 
        {
            _parkingSpaceRepository = parkingSpaceRepository;
            _parkedRepository = parkedRepository;
            _mapper = mapper;
        }

        public async Task<List<ParkingSpaceViewModel>> GetAllParkingSpaces()
        {
            try
            {
                List<ParkingSpace> parkingSpaces = await _parkingSpaceRepository.GetAllAsync();

                List<Parked> parkedActives = await _parkedRepository.GetAllParkedActive();

                var results = new List<ParkingSpaceViewModel>();

                foreach (var space in parkingSpaces)
                {
                    var parked = parkedActives.Where(x => x.ParkingSpaceId == space.Id).FirstOrDefault();

                    var result = _mapper.Map<ParkingSpaceViewModel>(space);

                    if (parked is not null)
                    {
                        _mapper.Map(parked, result);
                    }

                    results.Add(result);
                }

                return results;
            }
            catch
            {
                throw new Exception("ERR-PSS001 Falha interna no servidor");
            }
        }

        public async Task<List<ParkingSpaceViewModel>> CreateNewParkingSpaces(RangeInputModel range)
        {
            try
            {
                List<ParkingSpace> spaces = await PrepareList(range, true);

                await _parkingSpaceRepository.InsertRangeAsync(spaces);

                var results = new List<ParkingSpaceViewModel>();

                results.AddRange(spaces.Select(space => _mapper.Map<ParkingSpaceViewModel>(space)));

                return results;
            }
            catch
            {
                throw new Exception("ERR-PSS002 Falha interna no servidor");
            }
        }

        public async Task<List<ParkingSpaceViewModel>> ChangeParkingSpaceAvailability(ChangeParkingSpaceAvailability input)
        {
            try
            {
                List<ParkingSpace> spaces = await PrepareList(input);

                foreach (var space in spaces)
                    if (input.Active) space.EnableParkingSpace();
                    else space.DisableParkingSpace();

                await _parkingSpaceRepository.UpdateAsync(spaces);

                var results = new List<ParkingSpaceViewModel>();

                results.AddRange(spaces.Select(space => _mapper.Map<ParkingSpaceViewModel>(space)));

                return results;
            }
            catch
            {
                throw new Exception("ERR-PSS003 Falha interna no servidor");
            }
        }

        public async Task<List<ParkingSpaceViewModel>> ChangeReservationOfHandicapped(HandicapParkingSpaceInputModel input)
        {
            try
            {
                List<ParkingSpace> spaces = await PrepareList(input);

                foreach (var space in spaces)
                    if (input.Handicap) space.EnableHandicap();
                    else space.DisableHandicap();

                await _parkingSpaceRepository.UpdateAsync(spaces);

                var results = new List<ParkingSpaceViewModel>();

                results.AddRange(spaces.Select(space => _mapper.Map<ParkingSpaceViewModel>(space)));

                return results;
            }
            catch
            {
                throw new Exception("ERR-PSS004 Falha interna no servidor");
            }
        }

        private async Task<List<ParkingSpace>> PrepareList(RangeInputModel range, bool creation = false)
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