using Microsoft.EntityFrameworkCore;

namespace CondominiumParkingApi.Infrastructure.Data.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() 
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) 
            : base(options)
        {
        }
    }
}
