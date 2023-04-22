using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using CondominiumParkingApi.Infrastructure.Data.Contexts;

namespace CondominiumParkingApi.Infrastructure.Data.Repositories
{
    public class LimitExceededRepository : BaseRepository<LimitExceeded>, ILimitExceededRepository
    {
        public LimitExceededRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
