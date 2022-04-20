using AmenityProviderService.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AmenityProviderService.Commands.ServiceProviderAmenityOrderNotification
{
    public class AmenityOrderNotifyCommandHandler: IRequestHandler<AmenityOrderNotifyCommand, int>
    {
        private readonly IMapper _mapper;

        public AmenityOrderNotifyCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<int> Handle(AmenityOrderNotifyCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<OrderInfo>(request);
            await Console.Out.WriteLineAsync($"Dear UrbanClap Amenity provider, an order for amenity: {orderEntity.Amenity} has been placed at the location: {orderEntity.Location}. Grab the opportunity as per your availability! Hurry up and reply within next 30 minutes!");
            
            return 0;
        }
    }
}
