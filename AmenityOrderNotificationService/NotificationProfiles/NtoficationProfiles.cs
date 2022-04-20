using AmenityOrderNotificationService.Command;
using AutoMapper;
using EventBus.Messages.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderNotificationService.NotificationProfiles
{
    public class NtoficationProfiles: Profile
    {
        public NtoficationProfiles()
        {
            CreateMap<SendOrderContextToNotificationServiceEvent, OrderConfirmationNotificationCommand>();
        }
    }
}
