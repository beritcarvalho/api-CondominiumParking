using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using CondominiumParkingApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CondominiumParkingApi.Infrastructure.Data.Repositories
{
    public class ParkedRepository : BaseRepository<Parked>, IParkedRepository
    {
        public ParkedRepository(DataBaseContext context) : base(context)
        {
        }
    }

}