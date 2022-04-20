using AmenityOrderNotificationService.Command;
using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderNotificationService.EventConsumer
{
    public class SendOrderContextToNotificationServiceEventConsumer : IConsumer<SendOrderContextToNotificationServiceEvent>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SendOrderContextToNotificationServiceEventConsumer(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task Consume(ConsumeContext<SendOrderContextToNotificationServiceEvent> context)
        {
            var orderNotification = mapper.Map<OrderConfirmationNotificationCommand>(context.Message);
            await mediator.Send(orderNotification);
        }
    }
}
