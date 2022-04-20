using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityManagingService.EventConsumer
{
    public class AmenityOrderProviderResponseConsumer : IConsumer<AmenityOrderProviderResponseEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AmenityOrderProviderResponseConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AmenityOrderProviderResponseEvent> context)
        {
            await context.RespondAsync(new AmenityOrderManagerResponseEvent
            {
                CustomerId = context.Message.CustomerId,
                Amenity = context.Message.Amenity,
                IsAvailable = true,
                IsAccepted = context.Message.Accepted,
                ProviderName = context.Message.ProviderName,
                AmenityProviderId = context.Message.AmenityProviderId
            });
        }
    }
}
