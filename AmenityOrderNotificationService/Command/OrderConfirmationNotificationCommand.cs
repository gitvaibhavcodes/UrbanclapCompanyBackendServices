using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderNotificationService.Command
{
    public class OrderConfirmationNotificationCommand: IRequest<int>
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Amenity { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsAccepted { get; set; }
        public string ProviderName { get; set; }
        public int AmenityProviderId { get; set; }
    }
}
