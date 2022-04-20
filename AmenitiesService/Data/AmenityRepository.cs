using AmenityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmenityService.Data
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly AppDbContext _appDbContext;

        public AmenityRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; 
        }

        public void AddAmenity(Amenity amenity)
        {
            if (amenity == null)
            {
                throw new ArgumentNullException(nameof(amenity));
            }
            _appDbContext.Amenities.Add(amenity);
        }

        public IEnumerable<Amenity> GetAllAmenities()
        {
            return _appDbContext.Amenities.ToList();
        }

        public Amenity GetAmenityById(int id)
        {
            return _appDbContext.Amenities.FirstOrDefault(amenity => amenity.Id == id);
        }

        public bool SaveChanges()
        {
            return (_appDbContext.SaveChanges() >= 0);
        }
    }
}
