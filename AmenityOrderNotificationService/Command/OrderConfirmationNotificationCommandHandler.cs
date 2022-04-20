using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AmenityOrderNotificationService.Command
{
    public class OrderConfirmationNotificationCommandHandler : IRequestHandler<OrderConfirmationNotificationCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IBus bus;

        public OrderConfirmationNotificationCommandHandler(IMapper mapper, IBus bus)
        {
            this.mapper = mapper;
            this.bus = bus;
        }
        public async Task<int> Handle(OrderConfirmationNotificationCommand request, CancellationToken cancellationToken)
        {
            await bus.Publish<OrderPlacedNotificationEvent>(request);
            return 0;
        }
    }
}
