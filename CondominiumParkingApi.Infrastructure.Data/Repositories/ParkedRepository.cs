using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using CondominiumParkingApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace CondominiumParkingApi.Infrastructure.Data.Repositories
{
    public class ParkedRepository : BaseRepository<Parked>, IParkedRepository
    {
        public ParkedRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<Parked> GetInUseByParkingSpaceId(int parkingSpaceId) =>        
             await Context.Parkeds.Where(p => p.ParkingSpaceId == parkingSpaceId && p.Active).FirstOrDefaultAsync();
        
    }

}