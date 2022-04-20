using AmenityOrderingService.Dtos;
using AmenityOrderingService.Models;
using AutoMapper;
using CustomerService.Protos;
using EventBus.Messages.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.Profiles
{
    public class AmenityOrderingProfile: Profile
    {
        public AmenityOrderingProfile()
        {
            CreateMap<AmenityOrder, AmenityOrderReadDto>();
            CreateMap<AmenityOrderAddDto, AmenityOrder>();
            CreateMap<GrpcCustomerModel, Customer>();
            CreateMap<AmenityOrderManagerResponseEvent, AmenityOrderResponse>().ForMember(dest => dest.OrderId, act => act.Ignore());
        }
    }
}
