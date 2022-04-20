using AmenityManagingService.EventConsumer;
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

namespace AmenityManagingService
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
            services.AddMassTransit(config => {

                config.AddConsumer<AmenityOrderRequestConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBus:HostAddress"]);
                    cfg.UseHealthCheck(ctx);

                    cfg.ReceiveEndpoint(EventBusConstants.AmenityOrderFinalizerQueue, c => {
                        c.ConfigureConsumer<AmenityOrderRequestConsumer>(ctx);
                    });
                });
                //config.AddRequestClient<AmenityOrderFinalizerEvent>();
            });
            services.AddMassTransitHostedService();
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<AmenityOrderRequestConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmenityManagingService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmenityManagingService v1"));
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
