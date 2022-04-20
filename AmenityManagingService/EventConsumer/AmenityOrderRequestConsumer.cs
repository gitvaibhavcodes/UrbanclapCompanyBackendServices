using AmenityManagingService.Commands;
using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using MassTransit.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityManagingService.EventConsumer
{
    public class AmenityOrderRequestConsumer : IConsumer<AmenityOrderManagerRequestEvent>, IConsumer<AmenityOrderProviderResponseEvent>
    {
        //private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus bus;
        private readonly IDictionary<string, string> amenityRepository;

        public AmenityOrderRequestConsumer(/*IMediator mediator,*/ IMapper mapper, IPublishEndpoint publishEndpoint, IBus bus)
        {
            //_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint;
            this.bus = bus;
            amenityRepository = new Dictionary<string, string>()
            {
                { "AmenityA", "LocationA"},
                {"AmenityB", "LocationB" },
                { "AmenityC", "LocationC"},
                { "AmenityD", "LocationD"}
            };
        }

        public async Task Consume(ConsumeContext<AmenityOrderManagerRequestEvent> context)
        {
            //var command = _mapper.Map<AmenityOrderRequestCommand>(context.Message);
            bool isAmenityAvailable = CheckAmenityAvailability(context.Message.Amenity, context.Message.Location);
            if (isAmenityAvailable)
            {
                var eventMessage = new AmenityOrderNotifyEvent();
                eventMessage.Amenity = context.Message.Amenity;
                eventMessage.Location = context.Message.Location;
                var requestClient = bus.CreateRequestClient<AmenityOrderNotifyEvent>(RequestTimeout.After(m: 15));
                var amenityOrderAccepted = await requestClient.GetResponse<AmenityOrderProviderResponseEvent>(eventMessage);
                var managerResponse = new AmenityOrderManagerResponseEvent()
                {
                    CustomerId = amenityOrderAccepted.Message.CustomerId,
                    Amenity= amenityOrderAccepted.Message.Amenity,
                    IsAvailable = true,
                    IsAccepted = amenityOrderAccepted.Message.Accepted,
                    ProviderName = amenityOrderAccepted.Message.ProviderName,
                    AmenityProviderId = amenityOrderAccepted.Message.AmenityProviderId
                };
                await context.RespondAsync<AmenityOrderManagerResponseEvent>(managerResponse);
                //await _publishEndpoint.Publish(eventMessage);
            }
            else
            {
                await context.RespondAsync(new AmenityOrderManagerResponseEvent
                {
                    CustomerId = context.Message.CustomerId,
                    Amenity = context.Message.Amenity,
                    IsAvailable = isAmenityAvailable
                });
                //var result = await _mediator.Send(command);
            }
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
                AmenityProviderId = context.Message.AmenityProviderId,
            });
        }

        private bool CheckAmenityAvailability(string amenity, string customerLocation)
        {
            amenityRepository.TryGetValue(amenity, out string amenityLocation);
            return amenityLocation.Equals(customerLocation, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
