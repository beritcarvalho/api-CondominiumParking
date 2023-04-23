using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Domain.Interfaces
{
    public interface IParkedRepository : IBaseRepository<Parked>
    {
        Task<Parked> GetInUseByParkingSpaceId(int parkingSpaceId);
        Task<List<Parked>> GetAllParkedActive();
    }
}
