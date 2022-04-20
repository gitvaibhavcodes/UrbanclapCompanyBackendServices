using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.Dtos
{
    public class AmenityOrderAddDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Amenity { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
