using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Event
{
    public class OrderPlacedNotificationEvent: BaseEvent
    {
        public int OrderId { get; set; }
        public string Amenity { get; set; }        
        public string ProviderName { get; set; }
    }
}
