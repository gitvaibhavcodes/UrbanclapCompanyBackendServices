using AmenityProviderService.Commands.ServiceProviderAmenityOrderNotification;
using AmenityProviderService.Models;
using AutoMapper;
using EventBus.Messages.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityProviderService.Profiles
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<AmenityOrderNotifyEvent, AmenityOrderNotifyCommand>();
            CreateMap<AmenityOrderNotifyCommand, OrderInfo>();
            CreateMap<OrderProviderResponse ,AmenityOrderProviderResponseEvent>();
        }
    }
}
