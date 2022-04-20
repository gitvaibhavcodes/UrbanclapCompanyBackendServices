using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityProviderService.Models
{
    public class AmenityProvider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string Amenity { get; set; }
    }
}
