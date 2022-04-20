using AmenityService.Data;
using AmenityService.Dtos;
using AmenityService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController: ControllerBase
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public AmenityController(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetAmenityById")]
        public ActionResult<AmenityReadDto> GetAmenityById(int id)
        {
            var amenity = _amenityRepository.GetAmenityById(id);
            if (amenity != null)
            {
                return Ok(_mapper.Map<AmenityReadDto>(amenity));
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<AmenityReadDto>> GetAllAmenities()
        {
            Console.WriteLine("---- Getting all Amenities ----");

            var amenities = _amenityRepository.GetAllAmenities();
            return Ok(_mapper.Map<IEnumerable<AmenityReadDto>>(amenities));
        }

        [HttpPost]
        public ActionResult<AmenityReadDto> AddAmenity(AmenityAddDto amenityAddDto)
        {
            var amenity = _mapper.Map<Amenity>(amenityAddDto);
            _amenityRepository.AddAmenity(amenity);
            _amenityRepository.SaveChanges();

            var amenityReadDto = _mapper.Map<AmenityReadDto>(amenity);
            return CreatedAtRoute(nameof(GetAmenityById), new { Id = amenityReadDto.Id }, amenityReadDto);

            //throw new HttpRequestException("Internal server error");
        }

        
    }
}
