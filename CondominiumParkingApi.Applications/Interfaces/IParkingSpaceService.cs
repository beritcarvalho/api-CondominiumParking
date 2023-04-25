using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Interfaces
{
    public interface IParkingSpaceService
    {
        Task<List<ParkingSpaceViewModel>> CreateNewParkingSpaces(RangeInputModel range);
        Task<List<ParkingSpaceViewModel>> ChangeParkingSpaceAvailability(ChangeParkingSpaceAvailability input);
        Task<List<ParkingSpaceViewModel>> GetAllParkingSpaces();
        Task<List<ParkingSpaceViewModel>> ChangeReservationOfHandicapped(HandicapParkingSpaceInputModel input);
    }
}
