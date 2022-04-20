using AmenityProviderService.Models;
using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityProviderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityProviderController: ControllerBase
    {
        //private readonly IMapper _mapper;
        //private readonly IPublishEndpoint _publishEndpoint;
        //private readonly IBus bus;
        //private readonly IList<AmenityProvider> _amenityProvidersRepository;

        //public AmenityProviderController(IMapper mapper, IPublishEndpoint publishEndpoint, IBus bus)
        //{
        //    _mapper = mapper;
        //    _publishEndpoint = publishEndpoint;
        //    this.bus = bus;
        //    _amenityProvidersRepository = new List<AmenityProvider>()
        //    {
        //        new AmenityProvider
        //        {
        //            Id =1,
        //            ProviderName = "ProviderA",
        //            Amenity = "AmenityA"
        //        },
        //        new AmenityProvider
        //        {
        //            Id =2,
        //            ProviderName = "ProviderB",
        //            Amenity = "AmenityB"
        //        },
        //        new AmenityProvider
        //        {
        //            Id =3,
        //            ProviderName = "ProviderC",
        //            Amenity = "AmenityC"
        //        }
        //    };
        //}


        //[HttpPost]
        //public async Task<ActionResult<OrderProviderResponse>> RespondToOrder(OrderProviderResponse orderProviderResponse)
        //{
        //    var amenityProvider = _amenityProvidersRepository.FirstOrDefault(p=>p.Id == orderProviderResponse.AmenityProviderId);
        //    if (amenityProvider == null)
        //    {
        //        return BadRequest();
        //    }

        //    var eventMessage = _mapper.Map<AmenityOrderProviderResponseEvent>(orderProviderResponse);
        //    eventMessage.ProviderName = amenityProvider.ProviderName;
        //    await _publishEndpoint.Publish(eventMessage);
        //    return Ok(eventMessage);
        //}
    }
}
