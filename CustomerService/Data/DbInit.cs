using CustomerService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Data
{
    public static class DbInit
    {
        public static void InitializeDb(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Customers.Any())
            {
                Console.WriteLine("Seeding Data ...");
                context.Customers.AddRange(
                    new Customer()
                    {
                        Name = "Customer A",
                        Location = "CityA"
                    },
                    new Customer()
                    {
                        Name = "Customer B",
                        Location = "CityB"
                       
                    },
                    new Customer()
                    {
                        Name = "Customer C",
                        Location = "CityC"
                    },
                    new Customer()
                    {
                        Name = "Customer D",
                        Location = "CityD"
                    },
                    new Customer()
                    {
                        Name = "Customer E",
                        Location = "CityE"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
