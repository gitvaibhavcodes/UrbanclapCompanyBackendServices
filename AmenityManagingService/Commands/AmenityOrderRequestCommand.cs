using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityManagingService.Commands
{
    public class AmenityOrderRequestCommand
    {
        public int CustomerId { get; set; }
        public string Amenity { get; set; }
        public string Location { get; set; }
    }
}
