using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using CondominiumParkingApi.Infrastructure.Data.Contexts;

namespace CondominiumParkingApi.Infrastructure.Data.Repositories
{
    public class ParkedRepository : BaseRepository<Parked>, IParkedRepository
    {
        public ParkedRepository(DataBaseContext context) : base(context)
        {
        }
    }

}