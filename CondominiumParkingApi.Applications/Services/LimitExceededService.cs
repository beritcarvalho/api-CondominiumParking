using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Services
{
    public class LimitExceededService : ILimitExceededService
    {
        public Task<List<LimitExceededViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}