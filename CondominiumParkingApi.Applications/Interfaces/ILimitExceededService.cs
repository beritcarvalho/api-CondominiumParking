using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Interfaces
{
    public interface ILimitExceededService
    {
        Task<List<LimitExceededViewModel>> GetAll();
    }
}
