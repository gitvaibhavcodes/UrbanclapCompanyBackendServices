using AmenityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityService.Data
{
    public interface IAmenityRepository
    {
        bool SaveChanges();
        IEnumerable<Amenity> GetAllAmenities();
        Amenity GetAmenityById(int id);
        void AddAmenity(Amenity amenity);       


    }
}
