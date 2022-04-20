using EventBus.Messages.Event;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace CustomerService.EventConsumer
{
    public class OrderPlacedNotificationConsumer : IConsumer<OrderPlacedNotificationEvent>
    {
        public async Task Consume(ConsumeContext<OrderPlacedNotificationEvent> context)
        {
            await Console.Out.WriteLineAsync($"Your order has been placed for\nAmenity:{context.Message.Amenity}\nOrder: {context.Message.OrderId}, \nProvider Name: {context.Message.ProviderName}");
        }
    }
}
