using AmenityOrderingService.Models;
using EventBus.Messages.Event;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.Services
{
    public class AmenityOrderingHelper
    {
        private readonly IPublishEndpoint publishEndpoint;

        public AmenityOrderingHelper(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task SendOrderContextToNotificationService(AmenityOrderResponse amenityOrderResponse)
        {
            await publishEndpoint.Publish<SendOrderContextToNotificationServiceEvent>(amenityOrderResponse);
        }
    }
}
