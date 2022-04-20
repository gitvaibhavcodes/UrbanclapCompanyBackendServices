using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Event
{
    public class AmenityOrderProviderResponseEvent: BaseEvent
    {
        public int AmenityProviderId { get; set; }
        public bool Accepted { get; set; }
        public int CustomerId { get; set; }
        public string Location { get; set; }
        public string Amenity { get; set; }
        public string ProviderName { get; set; }
    }
}
