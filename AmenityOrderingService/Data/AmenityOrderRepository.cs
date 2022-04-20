using AmenityOrderingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.Data
{
    public class AmenityOrderRepository : IAmenityOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public AmenityOrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddAmenityOrder(AmenityOrder amenityOrder)
        {
            if (amenityOrder == null)
            {
                throw new ArgumentNullException(nameof(amenityOrder));
            }
            _appDbContext.AmenityOrders.Add(amenityOrder);
        }

        public IEnumerable<AmenityOrder> GetAllAmenityOrders()
        {
            return _appDbContext.AmenityOrders.ToList();
        }

        public AmenityOrder GetAmenityOrderById(int id)
        {
            return _appDbContext.AmenityOrders.FirstOrDefault(amenityOrder => amenityOrder.Id == id);
        }

        public bool SaveChanges()
        {
            return (_appDbContext.SaveChanges() >= 0);
        }
    }
}
