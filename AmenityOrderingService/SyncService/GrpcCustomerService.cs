using CustomerService.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService.SyncService
{
    public class GrpcCustomerService
    {
        private readonly CustomerProtoService.CustomerProtoServiceClient _customerProtoServiceClient;

        public GrpcCustomerService(CustomerProtoService.CustomerProtoServiceClient customerProtoServiceClient)
        {
            _customerProtoServiceClient = customerProtoServiceClient;
        }

        public async Task<CustomerResponse> GetCustomerById(int customerId)
        {
            return await _customerProtoServiceClient.GetCustomerAsync(new GetCustomerByIdRequest { Id = customerId});
        }
    }
}
