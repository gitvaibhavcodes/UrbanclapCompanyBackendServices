using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Event
{
    public class AmenityOrderManagerRequestEvent: BaseEvent
    {
        public int CustomerId { get; set; }
        public string Amenity { get; set; }
        public string Location { get; set; }
    }
}
