using AmenityService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityService.Dtos
{
    public class AmenityAddDto
    {
        [Required]
        public string Name { get; set; }
    }
}
