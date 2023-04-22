using CondominiumParkingApi.Applications.InputModels;
using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Interfaces
{
    public interface IParkedService
    {
        Task<List<ParkedViewModel>> GetAll();
        Task<ParkedViewModel> Park(ParkedInputModel entering);
        Task<ParkedViewModel> Unpark(ParkedInputModel leaving);
    }
}
