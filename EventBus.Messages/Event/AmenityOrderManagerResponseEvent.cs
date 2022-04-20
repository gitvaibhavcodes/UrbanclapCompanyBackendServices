using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Event
{
    public class AmenityOrderManagerResponseEvent: BaseEvent
    {
        public int CustomerId { get; set; }
        public string Amenity { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsAccepted { get; set; }
        public string ProviderName { get; set; }
        public int AmenityProviderId { get; set; }
    }
}
