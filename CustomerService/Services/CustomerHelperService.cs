using AutoMapper;
using CustomerService.Data;
using CustomerService.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Services
{
    public class CustomerHelperService: CustomerProtoService.CustomerProtoServiceBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerHelperService(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public override Task<CustomerResponse> GetCustomer(GetCustomerByIdRequest getCustomerByIdRequest, ServerCallContext context)
        {
            var response = new CustomerResponse();
            var customer = customerRepository.GetCustomerById(getCustomerByIdRequest.Id);

            response.Customer.Add(mapper.Map<GrpcCustomerModel>(customer));

            return Task.FromResult(response);
        }
    }
}
