using AmenityOrderingService.Models;
using Microsoft.EntityFrameworkCore;

namespace AmenityOrderingService.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AmenityOrder> AmenityOrders { get; set; }
    }
}
