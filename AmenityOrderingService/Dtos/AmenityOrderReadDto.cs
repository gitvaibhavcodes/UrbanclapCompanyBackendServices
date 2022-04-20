using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.Dtos
{
    public class AmenityOrderReadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Amenity { get; set; }
        public string Location { get; set; }
    }
}
