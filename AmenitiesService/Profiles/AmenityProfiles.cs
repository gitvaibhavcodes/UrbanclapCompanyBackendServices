using AmenityService.Dtos;
using AmenityService.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityService.Profiles
{
    public class AmenityProfiles: Profile
    {
        public AmenityProfiles()
        {
            CreateMap<Amenity, AmenityReadDto>();
            CreateMap<AmenityAddDto, Amenity>();
        }
    }
}
