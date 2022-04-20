using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityProviderService.Commands.ServiceProviderAmenityOrderNotification
{
    public class AmenityOrderNotifyCommand: IRequest<int>
    {
        public string Amenity { get; set; }
        public string Location { get; set; }
    }
}
