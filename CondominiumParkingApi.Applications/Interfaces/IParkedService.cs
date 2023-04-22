using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Interfaces
{
    public interface IParkedService
    {
        Task<List<ParkedViewModel>> GetAll();
    }
}
