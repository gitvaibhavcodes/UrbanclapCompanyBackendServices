using AutoMapper;
using CustomerService.Dtos;
using CustomerService.Models;
using CustomerService.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Profiles
{
    public class CustomerProfiles: Profile
    {
        public CustomerProfiles()
        {
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<CustomerAddDto, Customer>();
            CreateMap<Customer, GrpcCustomerModel>();
        }
    }
}
