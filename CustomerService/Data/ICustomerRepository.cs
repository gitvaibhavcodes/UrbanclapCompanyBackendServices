using CustomerService.Dtos;
using CustomerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Data
{
    public interface ICustomerRepository
    {
        bool SaveChanges();
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer, Customer customerUpdated);
    }
}
