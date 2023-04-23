using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Interfaces
{
    public interface IParkingSpaceService
    {
        Task<List<ParkingSpaceViewModel>> GetAll();
        Task<List<ParkingSpaceViewModel>> CreateNewParkingSpaces(ParkingSpaceInputModel range);
        Task<List<ParkingSpaceViewModel>> EnableByRange(ParkingSpaceInputModel range);
        Task<List<ParkingSpaceViewModel>> DisableByRange(ParkingSpaceInputModel range);            
        Task<List<ParkingSpaceViewModel>> GetAllParkingSpaces();
        Task<List<ParkingSpaceViewModel>> EnableHandcapByRange(ParkingSpaceInputModel range);
        Task<List<ParkingSpaceViewModel>> DisableHandcapByRange(ParkingSpaceInputModel range);
    }
}
