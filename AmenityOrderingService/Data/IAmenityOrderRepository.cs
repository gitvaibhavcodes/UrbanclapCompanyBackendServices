using AmenityOrderingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.Data
{
    public interface IAmenityOrderRepository
    {
        bool SaveChanges();
        IEnumerable<AmenityOrder> GetAllAmenityOrders();
        AmenityOrder GetAmenityOrderById(int id);
        void AddAmenityOrder(AmenityOrder amenityOrder);
    }
}
