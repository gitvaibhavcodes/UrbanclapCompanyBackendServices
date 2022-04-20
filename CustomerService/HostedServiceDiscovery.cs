//using Consul;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CustomerService
//{
//    public class HostedServiceDiscovery : IHostedService
//    {
//        public IConfiguration configuration;
//        private string registrationId;
//        private readonly IConsulClient consulClient;

//        public HostedServiceDiscovery(IConfiguration configuration)
//        {
//            this.configuration = configuration;
//            consulClient = new ConsulClient(x => {
//                x.Address = this.configuration.GetValue<Uri>("ServiceConfiguration:ServiceDiscoveryAddress");
//            });
//        }

//        public async Task StartAsync(CancellationToken cancellationToken)
//        {
//            var serviceName = configuration.GetValue<string>("ServiceConfiguration:ServiceName");
//            var serviceId = configuration.GetValue<string>("ServiceConfiguration:ServiceId");
//            var serviceAddress = configuration.GetValue<Uri>("ServiceConfiguration:ServiceAddress");
//            registrationId = serviceName + "-" + serviceId;

//            var agentService = new AgentServiceRegistration
//            {
//                ID = registrationId,
//                Name = serviceName,
//                Address = serviceAddress.Host,
//                Port = serviceAddress.Port
//            };
//            await consulClient.Agent.ServiceRegister(agentService, cancellationToken);
//        }

//        public async Task StopAsync(CancellationToken cancellationToken)
//        {
//            await consulClient.Agent.ServiceDeregister(registrationId, cancellationToken);
//        }
//    }
//}
