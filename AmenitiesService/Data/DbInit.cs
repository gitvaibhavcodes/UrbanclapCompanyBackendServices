using AmenityService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityService.Data
{
    public class DbInit
    {
        public static void InitializeDb(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Amenities.Any())
            {
                Console.WriteLine("Seeding Data ...");
                context.Amenities.AddRange(
                    new Amenity()
                    {
                        Name = "Amenity A",
                                       
                                          
                    },
                    new Amenity()
                    {
                        Name = "Amenity B",
                        
                    },
                    new Amenity()
                    {
                        Name = "Amenity C",
                        
                    },
                    new Amenity()
                    {
                        Name = "Amenity D",
                        
                    },
                    new Amenity()
                    {
                        Name = "Amenity E",
                       
                    }
                ); 
                context.SaveChanges();
            }
        }
    }
}
