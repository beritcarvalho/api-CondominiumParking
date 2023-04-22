using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.ViewModels;

namespace CondominiumParkingApi.Applications.Services
{
    public class ParkedService : IParkedService
    {
        public Task<List<ParkedViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}