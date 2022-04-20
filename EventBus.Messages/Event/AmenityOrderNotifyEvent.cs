using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Event
{
    public class AmenityOrderNotifyEvent: BaseEvent
    {
        public string Amenity { get; set; }
        public string Location { get; set; }
    }
}
