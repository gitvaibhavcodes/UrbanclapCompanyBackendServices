using AmenityOrderingService.AmenityOrderServices;
using AmenityOrderingService.Data;
using AmenityOrderingService.Services;
using AmenityOrderingService.SyncService;
using CustomerService.Protos;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmenityOrderingService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IHostedService, HostedServiceDiscovery>();
            services.AddScoped<AmenityOrderingHelper, AmenityOrderingHelper>();
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AmenityOrderDb"));
            services.AddScoped<IAmenityOrderRepository, AmenityOrderRepository>();
            services.AddScoped<IAmenityOrderService, AmenityOrderService>();
            services.AddGrpcClient<CustomerProtoService.CustomerProtoServiceClient>(client => { client.Address = new Uri("http://localhost:5000"); });
            services.AddScoped<GrpcCustomerService>();


            services.AddMassTransit(config => {
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBus:HostAddress"]);
                    cfg.UseHealthCheck(ctx);
                });
            });
            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmenityOrderingService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmenityOrderingService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
