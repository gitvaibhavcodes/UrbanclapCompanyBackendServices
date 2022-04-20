using CustomerService.Dtos;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            _appDbContext.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer, Customer customerUpdated)
        {
            customer.Name = customerUpdated.Name;
            customer.Location = customerUpdated.Location;
            
            _appDbContext.Entry(customer).State = EntityState.Modified;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _appDbContext.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _appDbContext.Customers.FirstOrDefault(cust => cust.Id == id);
        }

        public bool SaveChanges()
        {
            return (_appDbContext.SaveChanges() >= 0);
        }
    }
}
