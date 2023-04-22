using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Interfaces
{
    public interface IParkingSpaceService
    {
        Task<List<ParkingSpaceViewModel>> GetAll();
    }
}
