using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityProviderService.Models
{
    public class OrderProviderResponse
    {
        public int AmenityProviderId { get; set; }
        public bool Accepted { get; set; }
        public int CustomerId { get; set; }
        public string Location { get; set; }
        public string Amenity { get; set; }
        public string ProviderName { get; set; }
    }
}
