using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Common
{
    public static class EventBusConstants
    {
        public const string AmenityOrderFinalizerQueue = "amenityorder-finalizer-queue";
        public const string ServiceProviderAmenityNotifyQueue = "serviceprovider-amenitynotify-queue";
        public const string SendOrderContextToNotificationServiceEventQueue = "sendordercontext-toNotificationService-queue";
        public const string OrderPlacedNotificationEventQueue = "orderplaced-notificationevent-queue";
    }
}
