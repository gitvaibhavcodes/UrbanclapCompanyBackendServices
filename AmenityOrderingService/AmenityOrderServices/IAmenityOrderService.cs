using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.AmenityOrderServices
{
    public interface IAmenityOrderService
    {
        bool IsCustomerValid(int customerId);
        bool IsAmenityProviderAvailable(string amenity, string Location);
    }
}
