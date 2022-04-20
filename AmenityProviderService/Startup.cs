using AmenityProviderService.EventConsumer;
using EventBus.Messages.Common;
using EventBus.Messages.Event;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AmenityProviderService
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

            services.AddControllers();
            services.AddMassTransit(config => {

                config.AddConsumer<AmenityOrderNotifyEventConsumer>();
                config.AddConsumer<OrderPlacedNotificationConsumer>();
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBus:HostAddress"]);
                    cfg.UseHealthCheck(ctx);

                    cfg.ReceiveEndpoint(EventBusConstants.ServiceProviderAmenityNotifyQueue, c => {
                        c.ConfigureConsumer<AmenityOrderNotifyEventConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.OrderPlacedNotificationEventQueue, c => {
                        c.ConfigureConsumer<OrderPlacedNotificationConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmenityProviderService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmenityProviderService v1"));
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
