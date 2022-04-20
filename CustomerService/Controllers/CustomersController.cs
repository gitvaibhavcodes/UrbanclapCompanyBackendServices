using AutoMapper;
using CustomerService.Data;
using CustomerService.Dtos;
using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController: ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerReadDto> GetCustomerById(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if(customer != null)
            {
                return Ok(_mapper.Map<CustomerReadDto>(customer));
            }
            return NotFound();
        }

        [HttpGet]
        //[Route(("/getAllCustomers"))]
        public ActionResult<IEnumerable<CustomerReadDto>> GetAllCustomers()
        {
            Console.WriteLine("---- Getting all customers ----");

            var customers = _customerRepository.GetAllCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerReadDto>>(customers));
        }

        [HttpPost]
        public ActionResult<CustomerReadDto> AddCustomer(CustomerAddDto customerAddDto)
        {
            var customer = _mapper.Map<Customer>(customerAddDto);
            _customerRepository.AddCustomer(customer);
            _customerRepository.SaveChanges();

            var customerReadDto = _mapper.Map<CustomerReadDto>(customer);
            return CreatedAtRoute(nameof(GetCustomerById), new { Id = customerReadDto.Id }, customerReadDto);

            //throw new HttpRequestException("Internal server error");
        }

        [HttpPut("{id}")]
        public ActionResult<CustomerReadDto> UpdateCustomer(int id, CustomerAddDto customerUpdateDto)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer != null)
            {
                var customerModel = _mapper.Map<Customer>(customerUpdateDto);
                _customerRepository.UpdateCustomer(customer, customerModel);
                _customerRepository.SaveChanges();
                return Ok(_mapper.Map<CustomerReadDto>(customer));
            }
            return NotFound();
        }
    }
}
