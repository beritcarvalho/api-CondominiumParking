using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using CondominiumParkingApi.Infrastructure.Data.Contexts;

namespace CondominiumParkingApi.Infrastructure.Data.Repositories
{
    public class ParkingSpaceRepository : BaseRepository<ParkingSpace>, IParkingSpaceRepository
    {
        public ParkingSpaceRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
