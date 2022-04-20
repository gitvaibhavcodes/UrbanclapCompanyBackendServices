using AmenityProviderService.Commands.ServiceProviderAmenityOrderNotification;
using AmenityProviderService.Models;
using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityProviderService.EventConsumer
{
    public class AmenityOrderNotifyEventConsumer : IConsumer<AmenityOrderNotifyEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AmenityOrderNotifyEventConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<AmenityOrderNotifyEvent> context)
        {
            var command = _mapper.Map<AmenityOrderNotifyCommand>(context.Message);
            await _mediator.Send(command);
            await context.RespondAsync<AmenityOrderProviderResponseEvent>(new OrderProviderResponse()
            {
                 AmenityProviderId = 1,
                 Accepted = true,
                 CustomerId =1,
                Location = "LocationA",
                Amenity = "AmenityA",
                ProviderName = "ProviderA"
            });
        }
    }
}
